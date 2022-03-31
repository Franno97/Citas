using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mre.Sb.Cita.Cita
{
    /// <summary>
    /// Servicio para obtener la disponiblidad para el agendamiento de citas
    /// </summary>
    public interface IDisponibilidadAppService
    {
        /// <summary>
        /// Obtener los periodos de atencion disponibles para un servicio
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="unidadAdministrativaId"></param>
        /// <param name="servicioId"></param>
        /// <returns></returns>
        Task<List<PeriodoDisponibleDto>> ObtenerPeriodosDisponibles(ObtenerDisponibilidadEntrada entrada);


    }


}