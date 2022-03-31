using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mre.Sb.UnidadAdministrativa.HttpApi;
using System;
using Volo.Abp.Modularity;

namespace Mre.Sb.Appointment
{
    public static class RemoteServicesExtensions
    {

        public static void ConfigureHttpClient(
            ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
             
            //1. Opcion, con proxy dinamicos creados con Abp
            //context.Services.AddHttpClientProxy(
            //  typeof(IServiceTrakingAppService),
            //  "ServiceTraking"
            //);


            //2. Cliente creados con nswag
            var urlUnidadAdministrativa = configuration["RemoteServices:UnidadAdministrativa:BaseUrl"];

            context.Services.AddHttpClient<IVentanillaClient, VentanillaClient>(
                c =>
                {
                    c.BaseAddress = new Uri(urlUnidadAdministrativa); 
                })
              ;
        }

    }

   
}
