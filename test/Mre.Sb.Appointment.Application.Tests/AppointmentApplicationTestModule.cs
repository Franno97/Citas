using Volo.Abp.Modularity;

namespace Mre.Sb.Appointment
{
    [DependsOn(
        typeof(AppointmentApplicationModule),
        typeof(AppointmentDomainTestModule)
        )]
    public class AppointmentApplicationTestModule : AbpModule
    {

    }
}
