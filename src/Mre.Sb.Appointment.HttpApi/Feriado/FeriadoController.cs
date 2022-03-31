using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Feriado
{
    [RemoteService(Name = CitaRemoteServiceConsts.RemoteServiceName)]
    [Area("Cita")]
    [Route("api/cita/feriado")]
    [Authorize]
    public class FeriadoController : AppointmentController, IFeriadoAppService
    {
        private readonly IFeriadoAppService _feriadoAppService;

        public FeriadoController(IFeriadoAppService feriadoAppService)
        {
            _feriadoAppService = feriadoAppService;
        }

        [HttpPost]
        public async Task<FeriadoDto> CreateAsync(CreateUpdateFeriadoDto entrada)
        {
            return await _feriadoAppService.CreateAsync(entrada);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _feriadoAppService.DeleteAsync(id);
        }

        [HttpGet("{id}")]
        public async Task<FeriadoDto> GetAsync(Guid id)
        {
            return await _feriadoAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<FeriadoDto>> GetListAsync(GetFeriadoInput entrada)
        {
            return await _feriadoAppService.GetListAsync(entrada);
        }

        [HttpPut("{id}")]
        public async Task<FeriadoDto> UpdateAsync(Guid id, CreateUpdateFeriadoDto entrada)
        {
            return await _feriadoAppService.UpdateAsync(id, entrada);
        }

        [HttpGet("esFeriado")]
        public async Task<bool> EsFeriado(DateTime dia)
        {
            return await _feriadoAppService.EsFeriado(dia);
        }

    }
}
