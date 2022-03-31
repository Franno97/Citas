using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Mre.Sb.Cita.Feriado;
using Mre.Sb.Cita.Localization;
using Mre.Sb.UnidadAdministrativa.HttpApi;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.IdentityModel;

namespace Mre.Sb.Cita.Cita
{
    [Authorize]
    public class DisponibilidadAppService : ApplicationService, IDisponibilidadAppService
    {
        private readonly IServicioCalendarioAppService _servicioCalendario;

        private readonly ICitaAppService _citaAppService;
        private readonly IStringLocalizer<CitaResource> _localizer;
        private readonly IFeriadoAppService _feriadoAppService;
        private readonly IIdentityModelAuthenticationService _identityModelAuthenticationService;
        private readonly IVentanillaClient _ventanillaClient;

        public AbpIdentityClientOptions ClientOptions { get; }

        public DisponibilidadAppService(IServicioCalendarioAppService servicioCalendario,
            ICitaAppService citaAppService,
            IStringLocalizer<CitaResource> localizer,
            IFeriadoAppService feriadoAppService,
            IIdentityModelAuthenticationService identityModelAuthenticationService,
            IOptions<AbpIdentityClientOptions> abpIdentityClientOptions,
            IVentanillaClient ventanillaClient)
        {
            _servicioCalendario = servicioCalendario;
            _citaAppService = citaAppService;
            _localizer = localizer;
            _feriadoAppService = feriadoAppService;
            _identityModelAuthenticationService = identityModelAuthenticationService;
            ClientOptions = abpIdentityClientOptions.Value;
            _ventanillaClient = ventanillaClient;
        }

        public async Task<List<PeriodoDisponibleDto>> ObtenerPeriodosDisponibles(ObtenerDisponibilidadEntrada entrada)
        {
            var fecha = entrada.Fecha.Date;
            if (fecha < DateTime.Today)
            {
                throw new UserFriendlyException(string.Format(_localizer["Disponibilidad:FechaInvalidaMensaje"]));
            }

            var esFeriado = await _feriadoAppService.EsFeriado(fecha);
            if (esFeriado)
            {
                return new List<PeriodoDisponibleDto>
                {
                    new PeriodoDisponibleDto { Dia = fecha }
                };
            }

            var resultados = new List<PeriodoDisponibleDto>();

            //Obtienes los calendarios que existen para esa fecha
            var listaServiciosCalendarios = await _servicioCalendario.ObtenerPorServicioUnidadAdministrativa(
                new ObtenerServicioCalendarioEntrada
                {
                    UnidadAdministrativaId = entrada.UnidadAdministrativaId,
                    ServicioId = entrada.ServicioId,
                    Fecha = entrada.Fecha
                });

            var horariosDia = new List<TimeSpan>();

            if (listaServiciosCalendarios.Any())
            {
                //tomar solamente para el día de la semana que se está tomando
                var diaDeSemana = (((int)fecha.DayOfWeek == 0) ? 7 : (int)fecha.DayOfWeek).ToString();

                var servicioCalendario = listaServiciosCalendarios.First();

                var configuracionDiaria = servicioCalendario.PlanTrabajo.Configuraciones
                    .FirstOrDefault(x => x.Dia.Equals(diaDeSemana) && x.Horario != null);

                if (configuracionDiaria != null)
                {
                    var horaInicio = TimeSpan.Parse(configuracionDiaria.Horario.Inicio);
                    var horaFin = TimeSpan.Parse(configuracionDiaria.Horario.Fin);
                    var continuarHorario = true;
                    var duracion = TimeSpan.FromMinutes(servicioCalendario.Duracion);

                    var fechaActual = DateTime.Now;
                    var horaActual = new TimeSpan(fechaActual.Hour, fechaActual.Minute, fechaActual.Second);

                    var descansos = new List<HorarioDto>();
                    if(configuracionDiaria.Descanso != null && configuracionDiaria.Descanso.Descansos.Any())
                    {
                        descansos = configuracionDiaria.Descanso.Descansos.Select(x => new HorarioDto
                        {
                            Inicio = TimeSpan.Parse(x.Inicio),
                            Fin = TimeSpan.Parse(x.Fin)
                        }).ToList();
                    }

                    while (continuarHorario)
                    {
                        var horaFinTemporal = horaInicio.Add(duracion);

                        //Eliminar descansos
                        var esTiempoDescanso = descansos.Any(x => horaInicio >= x.Inicio && horaInicio < x.Fin);

                        if (!esTiempoDescanso)
                        {
                            if (horaFinTemporal > horaFin)
                            {
                                continuarHorario = false;
                            }
                            else if (!(fechaActual.Date == fecha.Date && horaActual > horaInicio))
                            {
                                horariosDia.Add(horaInicio);
                            }
                        }

                        horaInicio = horaFinTemporal;
                    }
                }
            }
            else
            {
                //.Add(new PeriodoDisponibleDto { Dia = fecha });
                //return resultados;
                throw new UserFriendlyException(string.Format(_localizer["Calendario:NoExiste"]));
            }


            //Obtener las citas para una fecha
            var obtenerCitaEntrada = new ObtenerCitaEntrada
            {
                ServicioId = entrada.ServicioId,
                UnidadAdministrativaId = entrada.UnidadAdministrativaId,
                Fecha = fecha
            };
            var citas = await _citaAppService.ObtenerPorServicioUnidadAdministrativa(obtenerCitaEntrada);
            //citas = citas.Where(x => x.Estado != EstadoCita.Cancelado).ToList();
            //var citasTemporales = citas.Select(x =>
            //new
            //{
            //    Inicio = new TimeSpan(x.Inicio.Hour, x.Inicio.Minute, x.Inicio.Second),
            //    Fin = new TimeSpan(x.Fin.Hour, x.Fin.Minute, x.Fin.Second)
            //}).ToList();

            var horariosDiaDisponibles = new List<TimeSpan>();
            var calendario = listaServiciosCalendarios.SingleOrDefault();
            if (calendario.UsarVentanillas)
            {
                horariosDiaDisponibles = await GenerarHorariosConVentanillas(citas, horariosDia, entrada.UnidadAdministrativaId);
            } else
            {
                horariosDiaDisponibles = GenerarHorariosDisponibles(citas, horariosDia);
            }

            

            //foreach (var horario in horariosDia)
            //{
            //    if (!citasTemporales.Any(x => horario == x.Inicio))
            //    {
            //        horariosDiaDisponibles.Add(horario);
            //    }
            //}

            var periodoDisponible = new PeriodoDisponibleDto { Dia = fecha };
            foreach (var item in horariosDiaDisponibles)
            {
                periodoDisponible.Horarios.Add(item.ToString(@"hh\:mm"));
            }

            if (!periodoDisponible.Horarios.Any())
            {
                throw new UserFriendlyException(string.Format(_localizer["Disponibilidad:NoExisteDisponibilidad"]));
            }

            resultados.Add(periodoDisponible);

            return resultados;
        }

        #region metodos soporte

        /// <summary>
        /// Dado un texto en hora se obtiene la fecha completa
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="texto"></param>
        /// <returns></returns>
        private DateTime ConvertirTextoAFecha(DateTime fecha, string texto)
        {
            string horaTemporalTexto;
            var minutosTemporalTexto = "0";
            var indiceSeparador = texto.IndexOf(":", StringComparison.Ordinal);
            if (indiceSeparador != -1)
            {
                horaTemporalTexto = texto.Remove(indiceSeparador);
                minutosTemporalTexto = texto.Remove(0, indiceSeparador + 1);
            }
            else
            {
                horaTemporalTexto = texto;
            }

            var horaTemporal = Convert.ToInt32(horaTemporalTexto);
            var minutosTemporal = Convert.ToInt32(minutosTemporalTexto);
            var fechaTemporal = fecha.AddHours(horaTemporal).AddMinutes(minutosTemporal);

            return fechaTemporal;
        }


        /// <summary>
        /// Generar los horarios disponibles para un dia
        /// </summary>
        private List<TimeSpan> GenerarHorariosDisponibles(List<CitaDto> citas, List<TimeSpan> horarios)
        {
            var horariosDiaDisponibles = new List<TimeSpan>();

            citas = citas.Where(x => x.Estado != EstadoCita.Cancelado).ToList();
            var citasTemporales = citas.Select(x =>
            new
            {
                Inicio = new TimeSpan(x.Inicio.Hour, x.Inicio.Minute, x.Inicio.Second),
                Fin = new TimeSpan(x.Fin.Hour, x.Fin.Minute, x.Fin.Second)
            }).ToList();

            foreach (var horario in horarios)
            {
                if (!citasTemporales.Any(x => horario == x.Inicio))
                {
                    horariosDiaDisponibles.Add(horario);
                }
            }

            return horariosDiaDisponibles;
        }

        /// <summary>
        /// Generar los horarios disponibles para un dia considerando numero de ventanillas
        /// </summary>
        private async Task<List<TimeSpan>> GenerarHorariosConVentanillas(List<CitaDto> citas, List<TimeSpan> horarios, Guid unidadAdministrativaId)
        {
            var token = await _identityModelAuthenticationService.GetAccessTokenAsync(GetClientConfiguration("VentanillaCliente"));

            _ventanillaClient.SetAccessToken(token);

            var ventanillas= await _ventanillaClient.ObtenerPorUnidadAdministrativaIdAsync(unidadAdministrativaId);

            var horariosDiaDisponibles = new List<TimeSpan>();

            citas = citas.Where(x => x.Estado != EstadoCita.Cancelado).ToList();
            var citasTemporales = citas.Select(x =>
            new
            {
                Inicio = new TimeSpan(x.Inicio.Hour, x.Inicio.Minute, x.Inicio.Second),
                Fin = new TimeSpan(x.Fin.Hour, x.Fin.Minute, x.Fin.Second)
            }).ToList();

            foreach (var horario in horarios)
            {
                var numeroCitas = citasTemporales.Count(x => horario == x.Inicio);

                if (numeroCitas < ventanillas.TotalCount)
                {
                    horariosDiaDisponibles.Add(horario);
                }
            }

            return horariosDiaDisponibles;
        }

        private IdentityClientConfiguration GetClientConfiguration(string identityClientName = null)
        {
            if (identityClientName.IsNullOrEmpty())
            {
                return ClientOptions.IdentityClients.Default;
            }

            return ClientOptions.IdentityClients.GetOrDefault(identityClientName) ??
                   ClientOptions.IdentityClients.Default;
        }

        #endregion metodos soporte
    }
}