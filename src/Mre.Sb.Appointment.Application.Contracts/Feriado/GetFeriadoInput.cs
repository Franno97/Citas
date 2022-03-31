using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Feriado
{
    public class GetFeriadoInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        [Required]
        public Guid UnidadAdministrativaCalendarioId { get; set; }
    }
}
