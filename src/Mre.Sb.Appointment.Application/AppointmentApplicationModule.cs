using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Mre.Sb.Cita;
using Volo.Abp.IdentityModel;

namespace Mre.Sb.Appointment
{
    [DependsOn(
        typeof(AppointmentDomainModule),
        typeof(AppointmentApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    [DependsOn(
        typeof(AbpIdentityModelModule))]
    public class AppointmentApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AppointmentApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AppointmentApplicationModule>(validate: true);
            });
        }
    }
}
