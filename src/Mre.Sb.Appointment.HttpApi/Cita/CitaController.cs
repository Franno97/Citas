using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Cita
{

    [RemoteService(Name = CitaRemoteServiceConsts.RemoteServiceName)]
    [Area("Cita")]
    [Route("api/cita/cita")]
    [Authorize]
    public class CitaController : AppointmentController, ICitaAppService
    {
        private readonly ICitaAppService _citaAppService;

        public CitaController(ICitaAppService citaAppService)
        {
            _citaAppService = citaAppService;
        }

        [HttpPost]
        public async Task<CitaDto> CreateAsync(CreateUpdateCitaDto entrada)
        {
            return await _citaAppService.CreateAsync(entrada);
        }

        [HttpDelete("{id}")]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public Task<CitaDto> GetAsync(Guid id)
        {
            return _citaAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<CitaDto>> GetListAsync(GetCitaInput entrada)
        {
            return await _citaAppService.GetListAsync(entrada);
        }

        [HttpPut("{id}")]
        public async Task<CitaDto> UpdateAsync(Guid id, CreateUpdateCitaDto entrada)
        {
            return await _citaAppService.UpdateAsync(id, entrada);
        }

        [HttpPost("existeCitaAgendada")]
        public async Task<bool> ExisteCitaAgendada(CreateUpdateCitaDto entrada)
        {
            return await _citaAppService.ExisteCitaAgendada(entrada);
        }

        [HttpGet("servicioUnidadAdministrativa")]
        public Task<List<CitaDto>> ObtenerPorServicioUnidadAdministrativa(ObtenerCitaEntrada entrada)
        {
            throw new NotImplementedException();
        }

        [HttpPut("reagendar")]
        public async Task<CitaDto> Reagendar(ReagendarCitaDto entrada)
        {
            return await _citaAppService.Reagendar(entrada);
        }

        [HttpPut("Cancelar/{id}")]
        public async Task<CitaDto> Cancelar(Guid id)
        {
            return await _citaAppService.Cancelar(id);
        }

        [HttpPost("agendar")]
        public async Task<CrearCitaSalida> Agendar(CreateUpdateCitaDto entrada)
        {
            return await _citaAppService.Agendar(entrada);
        }
    }
}
