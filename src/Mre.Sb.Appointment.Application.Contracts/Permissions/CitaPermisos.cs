using Volo.Abp.Reflection;

namespace Mre.Sb.Cita.Permisos
{
    public class CitaPermisos
    {
        public const string GroupName = "Cita";

        public static class Cita
        {
            public const string Default = GroupName + ".Cita";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }


        public static class UnidadAdministrativaCalendario
        {
            public const string Default = GroupName + ".UnidadAdministrativaCalendario";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }


        public static class ServicioCalendario
        {
            public const string Default = GroupName + ".ServicioCalendario";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class Feriado
        {
            public const string Default = GroupName + ".Feriado";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }


        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CitaPermisos));
        }
    }
}