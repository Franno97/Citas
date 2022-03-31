using System;
using System.ComponentModel.DataAnnotations;

namespace Mre.Sb.Cita.Cita
{
    /// <summary>
    /// Para reagendar una cita
    /// </summary>
    public class ReagendarCitaDto
    {
        /// <summary>
        /// Identificador
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Fecha y hora de inicio de la cita
        /// </summary>
        [Required]
        public DateTime Inicio { get; set; }
    }
}
