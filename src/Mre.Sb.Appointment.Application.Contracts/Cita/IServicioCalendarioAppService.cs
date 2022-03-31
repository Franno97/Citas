using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Mre.Sb.Cita.Cita
{
    public interface IServicioCalendarioAppService :
        ICrudAppService<ServicioCalendarioDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CrearActualizarServicioCalendarioDto>
    {
        public Task<ListResultDto<ServicioCalendarioLookupDto>> GetLookupAsync();

        public Task<List<ServicioCalendarioDto>> ObtenerPorServicioUnidadAdministrativa(
            ObtenerServicioCalendarioEntrada entrada);

        /// <summary>
        /// Si existe calendario por servicio y unidad administrativa
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        public Task<bool> ExistePorServicioUnidadAdministrativa(ObtenerServicioCalendarioEntrada entrada);
    }
}