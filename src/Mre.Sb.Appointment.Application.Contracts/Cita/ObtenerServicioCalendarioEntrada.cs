using System;
using System.ComponentModel.DataAnnotations;

namespace Mre.Sb.Cita.Cita
{
    /// <summary>
    /// Para buscar los calendarios por servicio y unidad administrativa
    /// </summary>
    public class ObtenerServicioCalendarioEntrada
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
