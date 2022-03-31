using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Mre.Sb.Cita.EntityFrameworkCore
{
    public class AppointmentHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<AppointmentHttpApiHostMigrationsDbContext>
    {
        public AppointmentHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<AppointmentHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Appointment"));

            return new AppointmentHttpApiHostMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
