using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Cita
{
    [RemoteService(Name = CitaRemoteServiceConsts.RemoteServiceName)]
    [Area("Cita")]
    [Route("api/cita/servicio-calendario")]
    [Authorize]
    public class ServicioCalendarioController : AppointmentController, IServicioCalendarioAppService
    {
        public IServicioCalendarioAppService ServicioCalendarioAppService { get; }

        private readonly IDisponibilidadAppService _disponibilidadAppService;


        public ServicioCalendarioController(IServicioCalendarioAppService servicioCalendarioAppService,
            IDisponibilidadAppService disponibilidadAppService)
        {
            ServicioCalendarioAppService = servicioCalendarioAppService;
            _disponibilidadAppService = disponibilidadAppService;
        }


        [HttpGet]
        public virtual Task<PagedResultDto<ServicioCalendarioDto>> GetListAsync(PagedAndSortedResultRequestDto entrada)
        {
            return ServicioCalendarioAppService.GetListAsync(entrada);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ServicioCalendarioDto> GetAsync(Guid id)
        {
            return ServicioCalendarioAppService.GetAsync(id);
        }

        [HttpPost]
        public async Task<ServicioCalendarioDto> CreateAsync(CrearActualizarServicioCalendarioDto entrada)
        {
            return await ServicioCalendarioAppService.CreateAsync(entrada);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ServicioCalendarioDto> UpdateAsync(Guid id, CrearActualizarServicioCalendarioDto entrada)
        {
            return ServicioCalendarioAppService.UpdateAsync(id, entrada);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return ServicioCalendarioAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("lookup")]
        public Task<ListResultDto<ServicioCalendarioLookupDto>> GetLookupAsync()
        {
            return ServicioCalendarioAppService.GetLookupAsync();
        }

        [HttpGet("servicioUnidadAdministrativa")]
        public async Task<List<ServicioCalendarioDto>> ObtenerPorServicioUnidadAdministrativa(
            ObtenerServicioCalendarioEntrada entrada)
        {
            return await ServicioCalendarioAppService.ObtenerPorServicioUnidadAdministrativa(entrada);
        }

        [HttpGet("disponibilidad")]
        public async Task<List<PeriodoDisponibleDto>> ObtenerPeriodosDisponibles(ObtenerDisponibilidadEntrada entrada)
        {
            return await _disponibilidadAppService.ObtenerPeriodosDisponibles(entrada);
        }

        [HttpGet("existe")]
        public async Task<bool> ExistePorServicioUnidadAdministrativa(ObtenerServicioCalendarioEntrada entrada)
        {
            return await ServicioCalendarioAppService.ExistePorServicioUnidadAdministrativa(entrada);
        }
    }
}