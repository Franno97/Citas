using System;
using System.Collections.Generic;

namespace Mre.Sb.Cita.Cita
{
    /// <summary>
    /// Para poder mostrar la dispponibilidad en un día
    /// </summary>
    public class PeriodoDisponibleDto
    {
        public PeriodoDisponibleDto()
        {
            Horarios = new List<string>();
        }

        /// <summary>
        /// Fecha del día
        /// </summary>
        public DateTime Dia { get; set; }

        /// <summary>
        /// Lista de los horarios disponibles
        /// </summary>
        public List<string> Horarios { get; set; }

    }
}
