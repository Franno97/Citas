using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Mre.Sb.Cita.Dominio
{
    /// <summary>
    /// Clase para guardar la configuracion del calendario para un servicio en una unidad administrativa
    /// Este calendario se utiliza para calcular la disponibilidad para las citas
    /// </summary>
    public class ServicioCalendario : Entity<Guid>
    {
        public ServicioCalendario(Guid unidadAdministrativaId, Guid servicioId, string planTrabajo,
            int duracion, DateTime inicioAgendamiento, DateTime finAgendamiento,
            int diasDisponibilidad, bool citaAutomatica, int horasGracia, bool usarVentanillas)
        {
            UnidadAdministrativaId = unidadAdministrativaId;
            ServicioId = servicioId;
            PlanTrabajo = planTrabajo;
            Duracion = duracion;
            InicioAgendamiento = inicioAgendamiento;
            FinAgendamiento = finAgendamiento;
            DiasDisponibilidad = diasDisponibilidad;
            CitaAutomatica = citaAutomatica;
            HorasGracia = horasGracia;
            UsarVentanillas = usarVentanillas;
        }

        protected ServicioCalendario()
        {
        }

        /// <summary>
        /// Unidad administrativa
        /// </summary>
        [Required]
        public virtual Guid UnidadAdministrativaId { get; set; }

        /// <summary>
        /// Servicio de una unidad administrativa
        /// </summary>
        [Required]
        public virtual Guid ServicioId { get; set; }

        /// <summary>
        /// Plan de trabajo para el servicio
        /// </summary>
        [Required]
        public virtual string PlanTrabajo { get; set; }

        /// <summary>
        /// Duracion de la cita para el servicio en minutos
        /// </summary>
        [Required]
        public virtual int Duracion { get; set; }

        /// <summary>
        /// Fecha desde la cual se puede agendar citas para el servicio
        /// </summary>
        [Required]
        public virtual DateTime InicioAgendamiento { get; set; }

        /// <summary>
        /// Fecha hasta la cual se puede agendarcitas para el servicio
        /// </summary>
        [Required]
        public virtual DateTime FinAgendamiento { get; set; }

        /// <summary>
        /// Numero de dias para generar disponibilidad, contados desde la fecha de inicio de agendamiento
        /// </summary>
        public virtual int DiasDisponibilidad { get; set; }

        /// <summary>
        /// Indica si se utiliza asignacion automatica para el servicio
        /// True el sistema devuelve la siguiente cita disponible, considerando horas de gracia
        /// </summary>
        public virtual bool CitaAutomatica { get; set; }

        /// <summary>
        /// Numero de horas que el sistema considera entre la generacion de cita y la siguiente cita disponible
        /// </summary>
        public virtual int HorasGracia { get; set; }


        /// <summary>
        /// Indica si se utiliza ventanillas para la asignacion de citas
        /// </summary>
        public virtual bool UsarVentanillas { get; set; }
    }
}
