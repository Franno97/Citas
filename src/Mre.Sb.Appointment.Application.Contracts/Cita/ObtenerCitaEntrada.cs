using System;
using System.ComponentModel.DataAnnotations;

namespace Mre.Sb.Cita.Cita
{
    /// <summary>
    /// Para obtener las citas para un servicio de una unidad administrativa
    /// </summary>
    public class ObtenerCitaEntrada
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
        public DateTime? Fecha { get; set; }
    }
}
