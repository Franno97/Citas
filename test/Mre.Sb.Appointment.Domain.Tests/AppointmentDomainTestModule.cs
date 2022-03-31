using Mre.Sb.Appointment.EntityFrameworkCore;
using Mre.Sb.Cita.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Mre.Sb.Appointment
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(AppointmentEntityFrameworkCoreTestModule)
        )]
    public class AppointmentDomainTestModule : AbpModule
    {
        
    }
}
