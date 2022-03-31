using System;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Feriado
{
    public class FeriadoDto : IEntityDto<Guid>
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Descripcion del feriado
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Fecha de inicio del feriado
        /// </summary>
        public DateTime Inicio { get; set; }

        /// <summary>
        /// Texto para mostrar la fecha de inicio
        /// </summary>
        public string InicioTexto { get; set; }

        /// <summary>
        /// Fecha de fin del feriado
        /// </summary>
        public DateTime Fin { get; set; }

        /// <summary>
        /// Texto para mostrar la fecha final
        /// </summary>
        public string FinTexto { get; set; }
    }
}
