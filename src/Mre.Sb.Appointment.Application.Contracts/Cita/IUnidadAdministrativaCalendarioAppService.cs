using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Mre.Sb.Cita.Cita
{
    public interface IUnidadAdministrativaCalendarioAppService : 
       ICrudAppService<UnidadAdministrativaCalendarioDto,
           Guid,
           PagedAndSortedResultRequestDto,
           CrearActualizarUnidadAdministrativaDto>
    {

    }
}
