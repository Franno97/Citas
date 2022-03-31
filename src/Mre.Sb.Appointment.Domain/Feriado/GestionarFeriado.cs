using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Mre.Sb.Cita.Feriado
{
    public class GestionarFeriado : DomainService
    {
        private readonly IRepository<Dominio.Feriado, Guid> _repositorio;

        public GestionarFeriado(IRepository<Dominio.Feriado, Guid> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Dominio.Feriado> CrearAsync(string descripcion, DateTime inicio, DateTime fin, Guid unidadAdministrativaCalendarioId)
        {
            var entidad = new Dominio.Feriado(descripcion: descripcion, inicio: inicio, fin: fin, unidadAdministrativaCalendarioId: unidadAdministrativaCalendarioId);

            return await Task.FromResult(entidad);
        }

    }
}
