using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Cita
{
    [RemoteService(Name = CitaRemoteServiceConsts.RemoteServiceName)]
    [Area("Cita")]
    [Route("api/cita/unidad-adminsitrativa-calendario")]
    [Authorize]
    public class UnidadAdministrativaCalendarioController
    {
        public IUnidadAdministrativaCalendarioAppService UnidadAdministrativaCalendarioAppService { get; }


        public UnidadAdministrativaCalendarioController(IUnidadAdministrativaCalendarioAppService unidadAdministrativaCalendarioAppService)
        {
            UnidadAdministrativaCalendarioAppService = unidadAdministrativaCalendarioAppService;
        }


        [HttpGet]
        public async Task<PagedResultDto<UnidadAdministrativaCalendarioDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await UnidadAdministrativaCalendarioAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<UnidadAdministrativaCalendarioDto> GetAsync(Guid id)
        {
            return UnidadAdministrativaCalendarioAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<UnidadAdministrativaCalendarioDto> CreateAsync(CrearActualizarUnidadAdministrativaDto input)
        {
            return await UnidadAdministrativaCalendarioAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<UnidadAdministrativaCalendarioDto> UpdateAsync(Guid id, CrearActualizarUnidadAdministrativaDto input)
        {
            return UnidadAdministrativaCalendarioAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return UnidadAdministrativaCalendarioAppService.DeleteAsync(id);
        }
    }
}
