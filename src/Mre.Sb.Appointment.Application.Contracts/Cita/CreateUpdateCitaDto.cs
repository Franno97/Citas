using System;
using System.ComponentModel.DataAnnotations;

namespace Mre.Sb.Cita.Cita
{
    public class CreateUpdateCitaDto
    {

        /// <summary>
        /// Unidad administrativa en donde se agenda la cita
        /// </summary>
        [Required]
        public Guid UnidadAdministrativaId { get; set; }

        /// <summary>
        /// Servicio asociado a la cita
        /// </summary>
        [Required]
        public Guid ServicioId { get; set; }

        /// <summary>
        /// Persona que agenda la cita
        /// </summary>
        public Guid PersonaId { get; set; }

        /// <summary>
        /// Funcionario que atiende la cita
        /// </summary>
        public Guid FuncionarioId { get; set; }

        /// <summary>
        /// Fecha y hora de inicio de la cita
        /// </summary>
        public DateTime Inicio { get; set; }

        /// <summary>
        /// Fecha y hora de fin de la cita
        /// </summary>
        public DateTime Fin { get; set; }

        /// <summary>
        /// Estado de la cita
        /// </summary>
        public EstadoCita Estado { get; set; }

    }
}
