using Mre.Sb.Cita.Localization;
using Volo.Abp.Application.Services;

namespace Mre.Sb.Appointment
{
    public abstract class AppointmentAppService : ApplicationService
    {
        protected AppointmentAppService()
        {
            LocalizationResource = typeof(CitaResource);
            ObjectMapperContext = typeof(AppointmentApplicationModule);
        }
    }
}
