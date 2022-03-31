using Microsoft.AspNetCore.Authorization;
using Mre.Sb.Cita.Dominio;
using Mre.Sb.Cita.Permisos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Mre.Sb.Cita.Cita
{
    [Authorize]
    public class UnidadAdministrativaCalendarioAppService :
        CrudAppService<
            Dominio.UnidadAdministrativaCalendario,
            UnidadAdministrativaCalendarioDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CrearActualizarUnidadAdministrativaDto>,
        IUnidadAdministrativaCalendarioAppService
    {
        private readonly GestionarUnidadAdministrativaCalendario gestionarUnidadAdministrativaCalendario;

        public UnidadAdministrativaCalendarioAppService(IRepository<UnidadAdministrativaCalendario, Guid> repositorio,
            GestionarUnidadAdministrativaCalendario gestionarUnidadAdministrativaCalendario)
            : base(repositorio)
        {
            this.gestionarUnidadAdministrativaCalendario = gestionarUnidadAdministrativaCalendario;

            //PermisosUnidadAdministrativaCalendario
            GetPolicyName = CitaPermisos.UnidadAdministrativaCalendario.Default;
            GetListPolicyName = CitaPermisos.UnidadAdministrativaCalendario.Default;
            CreatePolicyName = CitaPermisos.UnidadAdministrativaCalendario.Create;
            UpdatePolicyName = CitaPermisos.UnidadAdministrativaCalendario.Update;
            DeletePolicyName = CitaPermisos.UnidadAdministrativaCalendario.Delete;
        }

        public override async Task<UnidadAdministrativaCalendarioDto> CreateAsync(
            CrearActualizarUnidadAdministrativaDto entrada)
        {
            await CheckCreatePolicyAsync();

            var entidad = await gestionarUnidadAdministrativaCalendario.CreateAsync(
                unidadAdministrativaId: entrada.UnidadAdministrativaId,
                planTrabajo: Newtonsoft.Json.JsonConvert.SerializeObject(entrada.PlanTrabajo)
            );

            TryToSetTenantId(entidad);

            await Repository.InsertAsync(entidad, autoSave: true);

            return await GetAsync(entidad.Id);
        }
    }
}