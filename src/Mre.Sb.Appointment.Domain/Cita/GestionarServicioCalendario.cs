using Microsoft.Extensions.Localization;
using Mre.Sb.Cita.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Mre.Sb.Cita.Dominio
{
    public class GestionarServicioCalendario : DomainService
    {
        private readonly IRepository<ServicioCalendario, Guid> _repositorio;
        private readonly IStringLocalizer<CitaResource> _localizador;

        public GestionarServicioCalendario(IRepository<ServicioCalendario, Guid> repositorio, IStringLocalizer<CitaResource> localizador)
        {
            _repositorio = repositorio;
            _localizador = localizador;
        }
        public virtual async Task<ServicioCalendario> CreateAsync(Guid unidadAdministrativaId, Guid servicioId, string planTrabajo,
            int duracion, DateTime inicioAgendamiento, DateTime finAgendamiento,
            int diasDisponibilidad, bool citaAutomatica, int horasGracia, bool usarVentanillas)
        {
            var queryable = await _repositorio.GetQueryableAsync();
            var existeUnidadAdministrativaServicio = queryable.Any(s => s.UnidadAdministrativaId == unidadAdministrativaId && s.ServicioId == servicioId);

            if (existeUnidadAdministrativaServicio)
            {
                throw new UserFriendlyException(string.Format(_localizador["ServicioCalendario:ExisteUnidadAdministrativaServicio"]));
            }

            var entity = new ServicioCalendario(unidadAdministrativaId: unidadAdministrativaId, servicioId: servicioId,
                planTrabajo: planTrabajo, duracion: duracion,
                inicioAgendamiento: inicioAgendamiento, finAgendamiento: finAgendamiento,
                diasDisponibilidad: diasDisponibilidad, citaAutomatica: citaAutomatica, horasGracia: horasGracia, usarVentanillas: usarVentanillas);

            return entity;
        }
    }
}
