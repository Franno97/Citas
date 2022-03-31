namespace Mre.Sb.Cita
{
    /// <summary>
    /// Plan de trabajo por dia
    /// </summary>
    public class PlanDiario
    {
        /// <summary>
        /// Dia del plan
        /// </summary>
        public string Dia { get; set; }

        /// <summary>
        /// Horario de trabajo para el dia
        /// </summary>
        public Horario Horario { get; set; }

        /// <summary>
        /// Descanso establecidos para el dia
        /// </summary>
        public Descanso Descanso { get; set; }

    }
}
