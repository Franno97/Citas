using Mre.Sb.Cita;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Mre.Sb.Cita.EntityFrameworkCore
{
    [ConnectionStringName(CitaDbProperties.ConnectionStringName)]
    public interface ICitaDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}