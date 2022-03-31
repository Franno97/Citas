using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Mre.Sb.Cita.Localization;
using Mre.Sb.Cita.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Mre.Sb.Cita.Cita
{
    [Authorize]
    public class CitaAppService :
        CrudAppService<
            Dominio.Cita,
            CitaDto,
            Guid,
            GetCitaInput,
            CreateUpdateCitaDto>,
        ICitaAppService
    {
        private readonly IStringLocalizer<CitaResource> _localizer;
        private readonly GestionarCita _gestionarCita;
        private readonly ICurrentUser _currentUser;
        private readonly IServicioCalendarioAppService _servicioCalendarioAppService;
        private readonly ILogger _logger;

        public CitaAppService(IRepository<Dominio.Cita, Guid> repository,
            IStringLocalizer<CitaResource> localizer, GestionarCita gestionarCita,
            ICurrentUser currentUser, IServicioCalendarioAppService servicioCalendarioAppService,
            ILogger<CitaAppService> logger)
            : base(repository)
        {
            _localizer = localizer;
            _gestionarCita = gestionarCita;
            _currentUser = currentUser;
            _servicioCalendarioAppService = servicioCalendarioAppService;
            _logger = logger;

            //Permisos
            //GetPolicyName = CitaPermisos.Cita.Default;
            //GetListPolicyName = CitaPermisos.Cita.Default;
            //CreatePolicyName = CitaPermisos.Cita.Create;
            //UpdatePolicyName = CitaPermisos.Cita.Update;
            //DeletePolicyName = CitaPermisos.Cita.Delete;
        }


        public override async Task<CitaDto> UpdateAsync(Guid id, CreateUpdateCitaDto entrada)
        {
            await CheckUpdatePolicyAsync();

            var entity = await GetEntityByIdAsync(id);

            await MapToEntityAsync(entrada, entity);
            await Repository.UpdateAsync(entity, autoSave: true);

            return await GetAsync(entity.Id);
        }

        public override async Task<PagedResultDto<CitaDto>> GetListAsync(GetCitaInput entrada)
        {
            await CheckGetListPolicyAsync();

            var queryable = await CreateFilteredQueryAsync(entrada);

            queryable = queryable.Where(x => x.PersonaId == entrada.PersonaId);

            var totalCount = await AsyncExecuter.CountAsync(queryable);

            queryable = ApplySorting(queryable, entrada);
            queryable = ApplyPaging(queryable, entrada);

            var queryDto = queryable.Select(x => new CitaDto
            {
                Id = x.Id,
                UnidadAdministrativaId = x.UnidadAdministrativaId,
                ServicioId = x.ServicioId,
                Inicio = x.Inicio,
                Fin = x.Fin,
                Estado = x.Estado,
                EstadoNombre = x.Estado.ToString(),
                DiaCita = x.Inicio.Date,
                InicioHorario = x.Inicio.Hour.ToString() + ":" + (x.Inicio.Minute == 0 ? "00" : x.Inicio.Minute.ToString()),
                FinHorario = x.Fin.Hour.ToString() + ":" + (x.Fin.Minute == 0 ? "00" : x.Fin.Minute.ToString())
            }).OrderBy(x => x.Inicio);

            var entityDtos = await AsyncExecuter.ToListAsync(queryDto);

            return new PagedResultDto<CitaDto>(
                totalCount,
                entityDtos
            );
        }

        public async Task<bool> ExisteCitaAgendada(CreateUpdateCitaDto entrada)
        {
            entrada.PersonaId = entrada.PersonaId != Guid.Empty ? entrada.PersonaId : _currentUser.Id ?? new Guid();

            await CheckGetListPolicyAsync();

            var query = await Repository.GetQueryableAsync();

            var fechaHoy = DateTime.Today;

            var existe = query.Any(x => x.PersonaId == entrada.PersonaId && x.ServicioId == entrada.ServicioId
            && x.Inicio >= fechaHoy && x.Estado == EstadoCita.Registrado);

            return existe;
        }

        public async Task<List<CitaDto>> ObtenerPorServicioUnidadAdministrativa(ObtenerCitaEntrada entrada)
        {
            await CheckGetListPolicyAsync();

            var queryable = await Repository.GetQueryableAsync();
            queryable = queryable.Where(x => x.ServicioId == entrada.ServicioId
                                             && x.UnidadAdministrativaId == entrada.UnidadAdministrativaId);

            if (entrada.Fecha != null)
            {
                var fechaFiltro = entrada.Fecha.Value;
                queryable = queryable.Where(x => x.Inicio.Date == fechaFiltro);
            }


            var queryableDto = queryable.Select(x => new CitaDto
            {
                Id = x.Id,
                UnidadAdministrativaId = x.UnidadAdministrativaId,
                PersonaId = x.PersonaId,
                FuncionarioId = x.FuncionarioId,
                Inicio = x.Inicio,
                Fin = x.Fin,
                Estado = x.Estado
            });

            var entityDtos = await AsyncExecuter.ToListAsync(queryableDto);

            return entityDtos;
        }

        public async Task<CitaDto> Reagendar(ReagendarCitaDto entrada)
        {
            await CheckUpdatePolicyAsync();

            var entidad = await GetEntityByIdAsync(entrada.Id);

            var obtenerServicioCalendarioEntrada = new ObtenerServicioCalendarioEntrada
            {
                UnidadAdministrativaId = entidad.UnidadAdministrativaId,
                ServicioId = entidad.ServicioId,
                Fecha = entrada.Inicio.Date
            };

            var disponibilidades = await _servicioCalendarioAppService.ObtenerPorServicioUnidadAdministrativa(obtenerServicioCalendarioEntrada);

            var duracion = disponibilidades[0].Duracion;
            var fin = entrada.Inicio.AddMinutes(duracion);

            entidad = await _gestionarCita.ReagendarAsync(id: entrada.Id, inicio: entrada.Inicio, fin: fin);

            await Repository.UpdateAsync(entidad);

            return await GetAsync(entidad.Id);
        }

        public async Task<CitaDto> Cancelar(Guid id)
        {
            await CheckUpdatePolicyAsync();

            var entidad = await _gestionarCita.CancelarAsync(id);

            await Repository.UpdateAsync(entidad);

            return await GetAsync(entidad.Id);
        }

        public async Task<CrearCitaSalida> Agendar(CreateUpdateCitaDto entrada)
        {
            var respuesta = new CrearCitaSalida();

            try
            {
                var existeCita = await ExisteCitaSimilar(entrada);
                if (existeCita)
                {
                    var mensaje = string.Format(_localizer["GenerarCita:ErrorCitaNoDisponible"]);
                    _logger.LogError(mensaje);
                    respuesta.MensajeError = mensaje;
                    return respuesta;
                }

                //entrada.PersonaId = _currentUser.Id ?? new Guid();

                var obtenerServicioCalendarioEntrada = new ObtenerServicioCalendarioEntrada
                {

                    UnidadAdministrativaId = entrada.UnidadAdministrativaId,
                    ServicioId = entrada.ServicioId,
                    Fecha = entrada.Inicio.Date
                };

                var disponibilidades = await _servicioCalendarioAppService.ObtenerPorServicioUnidadAdministrativa(obtenerServicioCalendarioEntrada);

                var duracion = disponibilidades[0].Duracion;
                entrada.Fin = entrada.Inicio.AddMinutes(duracion);
                entrada.Estado = EstadoCita.Registrado;

                await CheckCreatePolicyAsync();

                var entidad = await _gestionarCita.CrearAsync(
                    unidadAdministrativaId: entrada.UnidadAdministrativaId,
                    servicioId: entrada.ServicioId,
                    personaId: entrada.PersonaId,
                    funcionarioId: null,
                    inicio: entrada.Inicio,
                    fin: entrada.Fin,
                    estado: entrada.Estado
                    );

                TryToSetTenantId(entidad);

                await Repository.InsertAsync(entidad, autoSave: true);
                respuesta.Satisfactorio = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AgendarCita - {ex.Message}");
                respuesta.MensajeError = _localizer["GenerarCita:ErrorPorDefecto"];
            }

            return respuesta;
        }

        /// <summary>
        /// Validar si existe una cita similar
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        private async Task<bool> ExisteCitaSimilar(CreateUpdateCitaDto entrada)
        {
            await CheckGetListPolicyAsync();

            var queryable = await Repository.GetQueryableAsync();

            var existe = queryable.Any(x => x.ServicioId == entrada.ServicioId && x.UnidadAdministrativaId == entrada.UnidadAdministrativaId
            && x.Inicio == entrada.Inicio && x.Estado == EstadoCita.Registrado);

            return existe;
        }

    }

}