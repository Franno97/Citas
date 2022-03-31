using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Mre.Sb.Cita
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AppointmentDomainSharedModule)
    )]
    public class AppointmentDomainModule : AbpModule
    {

    }
}
