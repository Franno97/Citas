using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace Mre.Sb.Cita.Dominio
{
    public class Cita : AuditedAggregateRoot<Guid>
    {
        protected Cita()
        {

        }

        public Cita(Guid id, Guid unidadAdministrativaId, Guid servicioId, Guid personaId,
            Guid? funcionarioId, DateTime inicio, DateTime fin, EstadoCita estado)
        {
            Id = id;
            UnidadAdministrativaId = unidadAdministrativaId;
            ServicioId = servicioId;
            PersonaId = personaId;
            FuncionarioId = funcionarioId;
            Inicio = inicio;
            Fin = fin;
            Estado = estado;
        }

        /// <summary>
        /// Reagendar una cita
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="fin"></param>
        public void Reagendar(DateTime inicio, DateTime fin)
        {
            Inicio = inicio;
            Fin = fin;
        }

        public void cancelar()
        {
            Estado = EstadoCita.Cancelado;
        }

        /// <summary>
        /// Unidad administrativa en donde se agenda la cita
        /// </summary>
        [Required]
        public virtual Guid UnidadAdministrativaId { get; set; }

        /// <summary>
        /// Servicio asociado a la cita
        /// </summary>
        [Required]
        public virtual Guid ServicioId { get; set; }

        /// <summary>
        /// Persona que agenda la cita
        /// </summary>
        [Required]
        public virtual Guid PersonaId { get; set; }

        /// <summary>
        /// Funcionario que atiende la cita
        /// </summary>
        public virtual Guid? FuncionarioId { get; set; }

        /// <summary>
        /// Fecha y hora de inicio de la cita
        /// </summary>
        [Required]
        public virtual DateTime Inicio { get; set; }

        /// <summary>
        /// Fecha y hora de fin de la cita
        /// </summary>
        [Required]
        public virtual DateTime Fin { get; set; }

        /// <summary>
        /// Estado de la cita
        /// </summary>
        [Required]
        public virtual EstadoCita Estado { get; set; }

    }
}
