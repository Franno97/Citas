using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Mre.Sb.Cita.Dominio
{
    /// <summary>
    /// Clase para informacion de dias feriados feriados
    /// </summary>
    public class Feriado: Entity<Guid>
    {
        public Feriado()
        {

        }

        public Feriado(string descripcion, DateTime inicio, DateTime fin, Guid unidadAdministrativaCalendarioId)
        {
            Descripcion = descripcion;
            Inicio = inicio;
            Fin = fin;
            UnidadAdministrativaCalendarioId = unidadAdministrativaCalendarioId;
        }

        /// <summary>
        /// Identificador del calendario de unidad administrativa al que corresponde el feriado
        /// </summary>
        [Required]
        public virtual Guid UnidadAdministrativaCalendarioId { get; set; }

        /// <summary>
        /// Descripcion del feriado
        /// </summary>
        [Required]
        public virtual string Descripcion { get; set; }

        /// <summary>
        /// Fecha de inicio del feriado
        /// </summary>
        [Required]
        public virtual DateTime Inicio { get; set; }

        /// <summary>
        /// Fecha de fin del feriado
        /// </summary>
        [Required]
        public virtual DateTime Fin { get; set; }

    }
}
