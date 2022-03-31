using System;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Cita
{
    public class GetCitaInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public Guid PersonaId { get; set; }

    }
}
