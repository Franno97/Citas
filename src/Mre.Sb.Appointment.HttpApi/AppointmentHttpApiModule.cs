using Localization.Resources.AbpUi;
using Mre.Sb.Cita.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Mre.Sb.Cita;

namespace Mre.Sb.Appointment
{
    [DependsOn(
        typeof(AppointmentApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class AppointmentHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AppointmentHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<CitaResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
