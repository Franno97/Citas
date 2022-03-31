using Mre.Sb.Cita;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Mre.Sb.Appointment
{
    [DependsOn(
        typeof(AppointmentHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class AppointmentConsoleApiClientModule : AbpModule
    {
        
    }
}
