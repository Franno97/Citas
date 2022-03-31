using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Mre.Sb.Cita.EntityFrameworkCore
{
    public class AppointmentHttpApiHostMigrationsDbContext : AbpDbContext<AppointmentHttpApiHostMigrationsDbContext>
    {
        public AppointmentHttpApiHostMigrationsDbContext(DbContextOptions<AppointmentHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureAppointment();
        }
    }
}
