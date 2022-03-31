using Microsoft.Extensions.DependencyInjection;
using Mre.Sb.Cita;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Mre.Sb.Cita.EntityFrameworkCore
{
    [DependsOn(
        typeof(AppointmentDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class AppointmentEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CitaDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddDefaultRepositories(includeAllEntities: true);
            });
        }
    }
}