using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Mre.Sb.Cita.Proceso
{
    [Authorize]
    public class AgendamientoAppService : ApplicationService, IAgendamientoAppService
    {
        public Task<string> SeleccionarServicio()
        {
            throw new NotImplementedException();
        }

        public Task<string> PresentarDisponiblidad()
        {
            throw new NotImplementedException();
        }

        public Task<string> AgendarCita()
        {
            throw new NotImplementedException();
        }

        

        
    }
}
