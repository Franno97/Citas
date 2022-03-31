using Mre.Sb.Cita.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Mre.Sb.Cita
{
    public abstract class AppointmentController : AbpController
    {
        protected AppointmentController()
        {
            LocalizationResource = typeof(CitaResource);
        }
    }
}
