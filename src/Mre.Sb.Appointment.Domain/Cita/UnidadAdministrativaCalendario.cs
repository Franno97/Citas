using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Mre.Sb.Cita.Dominio
{
    /// <summary>
    /// Clase para guardar informacion del calendario de la unidad administrativa
    /// </summary>
    public class UnidadAdministrativaCalendario : Entity<Guid>
    {
        public UnidadAdministrativaCalendario(Guid unidadAdministrativaId, string planTrabajo)
        {
            UnidadAdministrativaId = unidadAdministrativaId;
            PlanTrabajo = planTrabajo;
        }

        protected UnidadAdministrativaCalendario()
        {

        }


        /// <summary>
        /// Unidad administrativa
        /// </summary>
        [Required]
        public virtual Guid UnidadAdministrativaId { get; set; }


        /// <summary>
        /// Plan de trabajo de la unidad administrativa
        /// </summary>
        [Required]
        public virtual string PlanTrabajo { get; set; }


        /// <summary>
        /// Feriados asociados a la unidad administrativa
        /// </summary>
        public virtual ICollection<Feriado> Feriados { get; protected set; }

    }
}
