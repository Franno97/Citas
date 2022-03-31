using System;
using System.ComponentModel.DataAnnotations;

namespace Mre.Sb.Cita.Feriado
{
    public class CreateUpdateFeriadoDto
    {
        /// <summary>
        /// Descripcion del feriado
        /// </summary>
        [Required]
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha de inicio del feriado
        /// </summary>
        public DateTime Inicio { get; set; }

        /// <summary>
        /// Fecha de fin del feriado
        /// </summary>
        public DateTime Fin { get; set; }

        /// <summary>
        /// Identificador del calendario de la unidad administrativa a la que corresponde el feriado
        /// </summary>
        [Required]
        public Guid UnidadAdministrativaCalendarioId { get; set; }
    }
}
