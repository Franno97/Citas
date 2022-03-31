using System;
using System.ComponentModel.DataAnnotations;

namespace Mre.Sb.Cita.Cita
{
    public class CrearActualizarUnidadAdministrativaDto
    {
        [Required]
        public Guid UnidadAdministrativaId { get; set; }

        [Required]
        public PlanSemanal PlanTrabajo { get; set; }
    }
}
