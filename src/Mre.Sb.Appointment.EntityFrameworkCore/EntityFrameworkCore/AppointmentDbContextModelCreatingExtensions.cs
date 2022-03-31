using System;
using Microsoft.EntityFrameworkCore;
using Mre.Sb.Cita.Dominio;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Mre.Sb.Cita.EntityFrameworkCore
{
    public static class AppointmentDbContextModelCreatingExtensions
    {
        public static void ConfigureAppointment(
            this ModelBuilder builder,
            Action<AppointmentModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new AppointmentModelBuilderConfigurationOptions(
                CitaDbProperties.DbTablePrefix,
                CitaDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */

            builder.Entity<ServicioCalendario>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "ServicioCalendario", options.Schema);

                b.ConfigureByConvention();
            });

            builder.Entity<UnidadAdministrativaCalendario>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "UnidadAdministrativaCalendario", options.Schema);

                b.HasMany(f => f.Feriados).WithOne().HasForeignKey(uf => uf.UnidadAdministrativaCalendarioId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                b.ConfigureByConvention();
            });

            builder.Entity<Dominio.Cita>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Cita", options.Schema);

                b.ConfigureByConvention();
            });

            builder.Entity<Dominio.Feriado>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Feriado", options.Schema);

                b.ConfigureByConvention();
            });

        }
    }
}