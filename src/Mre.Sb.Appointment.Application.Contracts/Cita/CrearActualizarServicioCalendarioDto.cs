using System;

namespace Mre.Sb.Cita.Cita
{
    public class CrearActualizarServicioCalendarioDto
    {
        public Guid UnidadAdministrativaId { get; set; }

        public Guid ServicioId { get; set; }

        public PlanSemanal PlanTrabajo { get; set; }

        public int Duracion { get; set; }

        public DateTime InicioAgendamiento { get; set; }

        public DateTime FinAgendamiento { get; set; }

        public int DiasDisponibilidad { get; set; }

        public bool CitaAutomatica { get; set; }

        public int HorasGracia { get; set; }

        public bool UsarVentanillas { get; set; }

    }
}
