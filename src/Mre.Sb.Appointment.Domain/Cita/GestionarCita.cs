using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Mre.Sb.Cita.Dominio;
using Mre.Sb.Cita.Localization;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Mre.Sb.Cita.Cita
{
    public class GestionarCita : DomainService
    {
        private readonly IRepository<Dominio.Cita, Guid> _repositorio;
        private readonly IStringLocalizer<CitaResource> _localizador;

        public GestionarCita(IRepository<Dominio.Cita, Guid> repositorio, IStringLocalizer<CitaResource> localizador)
        {
            _repositorio = repositorio;
            _localizador = localizador;
        }

        public async Task<Dominio.Cita> CrearAsync(Guid unidadAdministrativaId, Guid servicioId, Guid personaId,
            Guid? funcionarioId, DateTime inicio, DateTime fin, EstadoCita estado)
        {
            var queryable = await _repositorio.GetQueryableAsync();

            var fechaHoy = DateTime.Today;

            var existe = queryable.Any(x => x.PersonaId == personaId && x.ServicioId == servicioId
            && x.Inicio >= fechaHoy && x.Estado == EstadoCita.Registrado);
            if (existe)
            {
                throw new UserFriendlyException(string.Format(_localizador["GenerarCita:CitaExistenteError"]));
            }

            existe = queryable.Any(x => x.UnidadAdministrativaId == unidadAdministrativaId && x.ServicioId == servicioId
            && x.Inicio == inicio && x.Estado == EstadoCita.Registrado);
            if (existe)
            {
                throw new UserFriendlyException(string.Format(_localizador["GenerarCita:ErrorCitaNoDisponible"]));
            }

            var entidad = new Dominio.Cita(id: Guid.NewGuid(), unidadAdministrativaId: unidadAdministrativaId, servicioId: servicioId, personaId: personaId,
                funcionarioId: funcionarioId, inicio: inicio, fin: fin, estado: estado);

            return await Task.FromResult(entidad);
        }

        public async Task<Dominio.Cita> ReagendarAsync(Guid id, DateTime inicio, DateTime fin)
        {
            var queryable = await _repositorio.GetQueryableAsync();

            var query = queryable.Where(x => x.Id == id);

            var cita = await AsyncExecuter.FirstAsync(query);

            var fechaHoy = DateTime.Today;
            var existe = queryable.Any(x => x.Id != id && x.PersonaId == cita.PersonaId
            && x.ServicioId == cita.ServicioId && x.Inicio >= fechaHoy);
            if (existe)
            {
                throw new UserFriendlyException(string.Format(_localizador["GenerarCita:CitaExistenteError"]));
            }

            cita.Reagendar(inicio, fin);

            return cita;
        }

        public async Task<Dominio.Cita> CancelarAsync(Guid id)
        {
            var queryable = await _repositorio.GetQueryableAsync();

            var query = queryable.Where(x => x.Id == id);

            var cita = await AsyncExecuter.FirstAsync(query);

            cita.cancelar();

            return cita;
        }
    }
}
