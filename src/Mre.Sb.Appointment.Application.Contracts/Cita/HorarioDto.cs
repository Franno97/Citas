using System;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.Cita.Cita
{
    public class HorarioDto : IEntityDto
    {
        /// <summary>
        /// Hora de inicio
        /// </summary>
        public TimeSpan Inicio { get; set; }


        /// <summary>
        /// Hora de fin
        /// </summary>
        public TimeSpan Fin { get; set; }
    }
}
