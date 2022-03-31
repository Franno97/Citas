using System;
using System.ComponentModel.DataAnnotations;

namespace Mre.Sb.Cita.Cita
{
    /// <summary>
    /// Para obtner las disponibilidades de un servicio
    /// </summary>
    public class ObtenerDisponibilidadEntrada
    {
        /// <summary>
        /// Unidad administrativa
        /// </summary>
        [Required]
        public Guid UnidadAdministrativaId { get; set; }

        /// <summary>
        /// Servicio de una unidad administrativa
        /// </summary>
        [Required]
        public Guid ServicioId { get; set; }

        /// <summary>
        /// La fecha
        /// </summary>
        [Required]
        public DateTime Fecha { get; set; }
    }
}