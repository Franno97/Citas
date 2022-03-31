using System;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Cita
{
    public class CitaDto : IEntityDto<Guid>
    {
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Unidad administrativa en donde se agenda la cita
        /// </summary>
        public Guid UnidadAdministrativaId { get; set; }

        /// <summary>
        /// Nombre de la unidad administrativa
        /// </summary>
        public string UnidadAdministrativaNombre { get; set; }

        /// <summary>
        /// Servicio asociado a la cita
        /// </summary>
        public Guid ServicioId { get; set; }

        /// <summary>
        /// Nombre del servicio
        /// </summary>
        public string ServicioNombre { get; set; }

        /// <summary>
        /// Persona que agenda la cita
        /// </summary>
        public Guid PersonaId { get; set; }

        /// <summary>
        /// Funcionario que atiende la cita
        /// </summary>
        public Guid? FuncionarioId { get; set; }

        /// <summary>
        /// Fecha y hora de inicio de la cita
        /// </summary>
        public DateTime Inicio { get; set; }

        /// <summary>
        /// Horario de inicio
        /// </summary>
        public string InicioHorario { get; set; }

        /// <summary>
        /// Fecha y hora de fin de la cita
        /// </summary>
        public DateTime Fin { get; set; }

        /// <summary>
        /// Fin del horario
        /// </summary>
        public string FinHorario { get; set; }

        /// <summary>
        /// Estado de la cita
        /// </summary>
        public EstadoCita Estado { get; set; }

        /// <summary>
        /// Texto del estado
        /// </summary>
        public string EstadoNombre { get; set; }

        /// <summary>
        /// Día de la cita
        /// </summary>
        public DateTime DiaCita { get; set; }
    }
}
