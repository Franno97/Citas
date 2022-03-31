using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Mre.Sb.Cita.EntityFrameworkCore
{
    public class AppointmentModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public AppointmentModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}