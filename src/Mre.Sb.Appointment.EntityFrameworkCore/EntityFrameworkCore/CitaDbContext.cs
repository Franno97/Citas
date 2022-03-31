using Audit.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Mre.Sb.Cita.Dominio;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Mre.Sb.Cita.EntityFrameworkCore
{
    [ConnectionStringName(CitaDbProperties.ConnectionStringName)]
    public class CitaDbContext : AbpDbContext<CitaDbContext>, ICitaDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Dominio.Cita> Cita { get; set; }
        public DbSet<Dominio.Feriado> Feriado { get; set; }
        public DbSet<ServicioCalendario> ServicioCalendario { get; set; }
        public DbSet<UnidadAdministrativaCalendario> UnidadAdministrativaCalendario { get; set; }

        public CitaDbContext(DbContextOptions<CitaDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureAppointment();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new AuditSaveChangesInterceptor());
        }
    }
}