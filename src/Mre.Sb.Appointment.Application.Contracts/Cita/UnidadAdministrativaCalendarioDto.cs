using System;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Cita
{
    public class UnidadAdministrativaCalendarioDto : IEntityDto<Guid>
    {
        public Guid Id { get; set; }

        public Guid UnidadAdministrativaId { get; set; }

        public string UnidadAdministrativaNombre { get; set; }

        public PlanSemanal PlanTrabajo { get; set; }
    }
}
