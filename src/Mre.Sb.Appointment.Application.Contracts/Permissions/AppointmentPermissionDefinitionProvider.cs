using Mre.Sb.Cita.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Mre.Sb.Cita.Permisos
{
    public class AppointmentPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var moduleGroup = context.AddGroup(CitaPermisos.GroupName, L("Permiso:Cita"));

            var feriadoPermission = moduleGroup.AddPermission(CitaPermisos.Feriado.Default, L("Permiso:AdministracionFeriado"));
            feriadoPermission.AddChild(CitaPermisos.Feriado.Create, L("Permiso:Creacion"));
            feriadoPermission.AddChild(CitaPermisos.Feriado.Update, L("Permiso:Edicion"));
            feriadoPermission.AddChild(CitaPermisos.Feriado.Delete, L("Permiso:Eliminacion"));

            var citaPermission = moduleGroup.AddPermission(CitaPermisos.Cita.Default, L("Permiso:AdministracionCita"));
            citaPermission.AddChild(CitaPermisos.Cita.Create, L("Permiso:Creacion"));
            citaPermission.AddChild(CitaPermisos.Cita.Update, L("Permiso:Edicion"));
            citaPermission.AddChild(CitaPermisos.Cita.Delete, L("Permiso:Eliminacion"));

            var servicioCalendarioPermission = moduleGroup.AddPermission(CitaPermisos.ServicioCalendario.Default, L("Permiso:AdministracionServicioCalendario"));
            servicioCalendarioPermission.AddChild(CitaPermisos.ServicioCalendario.Create, L("Permiso:Creacion"));
            servicioCalendarioPermission.AddChild(CitaPermisos.ServicioCalendario.Update, L("Permiso:Edicion"));
            servicioCalendarioPermission.AddChild(CitaPermisos.ServicioCalendario.Delete, L("Permiso:Eliminacion"));

            var unidadAdminCalendarioPermission = moduleGroup.AddPermission(CitaPermisos.UnidadAdministrativaCalendario.Default, L("Permiso:AdministracionUnidadAdminCalendario"));
            unidadAdminCalendarioPermission.AddChild(CitaPermisos.UnidadAdministrativaCalendario.Create, L("Permiso:Creacion"));
            unidadAdminCalendarioPermission.AddChild(CitaPermisos.UnidadAdministrativaCalendario.Update, L("Permiso:Edicion"));
            unidadAdminCalendarioPermission.AddChild(CitaPermisos.UnidadAdministrativaCalendario.Delete, L("Permiso:Eliminacion"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CitaResource>(name);
        }
    }
}