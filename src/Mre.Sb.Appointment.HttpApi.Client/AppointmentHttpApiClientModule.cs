using Microsoft.Extensions.DependencyInjection;
using Mre.Sb.Cita;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Mre.Sb.Cita
{
    [DependsOn(
        typeof(AppointmentApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class AppointmentHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Cita";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AppointmentApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
