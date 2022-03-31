using Microsoft.Extensions.Localization;
using Mre.Sb.Cita.Dominio;
using Mre.Sb.Cita.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Mre.Sb.Cita.Dominio
{
    public class GestionarUnidadAdministrativaCalendario : DomainService
    {
        private readonly IRepository<UnidadAdministrativaCalendario, Guid> repositorio;
        private readonly IStringLocalizer<CitaResource> localizador;

        public GestionarUnidadAdministrativaCalendario(IRepository<UnidadAdministrativaCalendario, Guid> repositorio, IStringLocalizer<CitaResource> localizador)
        {
            this.repositorio = repositorio;
            this.localizador = localizador;
        }
        public virtual async Task<UnidadAdministrativaCalendario> CreateAsync(Guid unidadAdministrativaId, string planTrabajo)
        {
            var queryable = await repositorio.GetQueryableAsync();
            var existeUnidadAdministrativaServicio = queryable.Any(s => s.UnidadAdministrativaId == unidadAdministrativaId);

            if (existeUnidadAdministrativaServicio)
            {
                throw new UserFriendlyException(string.Format(localizador["UnidadAdministrativaCalendario:ExisteUnidadAdministrativaCalendario"]));
            }

            var entity = new UnidadAdministrativaCalendario(unidadAdministrativaId: unidadAdministrativaId, planTrabajo: planTrabajo);

            return entity;
        }
    }
}
