using System;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Cita
{
    public class ServicioCalendarioDto : IEntityDto<Guid>
    {
        public Guid Id { get; set; }
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

    public class ServicioCalendarioLookupDto : EntityDto<Guid>
    {
        public Guid UnidadAdministrativaId { get; set; }

        public Guid ServicioId { get; set; }
    }
}
