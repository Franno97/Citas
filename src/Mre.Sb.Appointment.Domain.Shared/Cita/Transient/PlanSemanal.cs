using System.Collections.Generic;

namespace Mre.Sb.Cita
{
    /// <summary>
    /// Plan de trabajo por semana
    /// </summary>
    public class PlanSemanal
    {
        /// <summary>
        /// Lista de planes diarios que conforman el plan de la semana
        /// </summary>
        public ICollection<PlanDiario> Configuraciones { get; set; }

    }
}
