using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mre.Sb.Cita.Proceso
{
    public interface IAgendamientoAppService
    {
        Task<string> SeleccionarServicio();
        Task<string> PresentarDisponiblidad();
        Task<string> AgendarCita();

    }
}
