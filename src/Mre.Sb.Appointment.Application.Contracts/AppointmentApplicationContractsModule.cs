using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Mre.Sb.Cita
{
    [DependsOn(
        typeof(AppointmentDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class AppointmentApplicationContractsModule : AbpModule
    {

    }
}
