using AutoMapper;
using Mre.Sb.Cita;
using Mre.Sb.Cita.Cita;
using Mre.Sb.Cita.Dominio;
using Mre.Sb.Cita.Feriado;
using Volo.Abp.AutoMapper;

namespace Mre.Sb.Appointment
{
    public class AppointmentApplicationAutoMapperProfile : Profile
    {
        public AppointmentApplicationAutoMapperProfile()
        {
            CreateMap<ServicioCalendario, ServicioCalendarioLookupDto>();
            CreateMap<ServicioCalendario, ServicioCalendarioDto>()
                .ForMember(x => x.PlanTrabajo, map => map.MapFrom(e => Newtonsoft.Json.JsonConvert.DeserializeObject<PlanSemanal>(e.PlanTrabajo)));
            CreateMap<CrearActualizarServicioCalendarioDto, ServicioCalendario>()
                .ForMember(x => x.PlanTrabajo, map => map.MapFrom(e => Newtonsoft.Json.JsonConvert.SerializeObject(e.PlanTrabajo)))
                .ForMember(x => x.Id, map => map.Ignore());

            CreateMap<UnidadAdministrativaCalendario, UnidadAdministrativaCalendarioDto>()
                .ForMember(x => x.PlanTrabajo, map => map.MapFrom(e => Newtonsoft.Json.JsonConvert.DeserializeObject<PlanSemanal>(e.PlanTrabajo)))
                .ForMember(x => x.UnidadAdministrativaNombre, map => map.Ignore());
            CreateMap<CrearActualizarUnidadAdministrativaDto, UnidadAdministrativaCalendario>()
                .ForMember(x => x.PlanTrabajo, map => map.MapFrom(e => Newtonsoft.Json.JsonConvert.SerializeObject(e.PlanTrabajo)))
                .ForMember(x => x.Id, map => map.Ignore())
                .ForMember(x => x.Feriados, map => map.Ignore());

            CreateMap<Cita.Dominio.Cita, CitaDto>()
                .ForMember(x => x.UnidadAdministrativaNombre, map => map.Ignore())
                .ForMember(x => x.ServicioNombre, map => map.Ignore())
                .ForMember(x => x.EstadoNombre, map => map.MapFrom(e => e.Estado.ToString()))
                .ForMember(x => x.DiaCita, map => map.MapFrom(s => s.Inicio.Date))
                .ForMember(x => x.InicioHorario, map => map.MapFrom(s => s.Inicio.Hour.ToString() + ":" + (s.Inicio.Minute == 0 ? "00" : s.Inicio.Minute.ToString())))
                .ForMember(x => x.FinHorario, map => map.MapFrom(s => s.Fin.Hour.ToString() + ":" + (s.Fin.Minute == 0 ? "00" : s.Fin.Minute.ToString())));
            CreateMap<CreateUpdateCitaDto, Cita.Dominio.Cita>()
            .ForMember(x => x.Id, map => map.Ignore())
            .ForMember(x => x.ConcurrencyStamp, map => map.Ignore())
            .ForMember(x => x.ExtraProperties, map => map.Ignore())
            .IgnoreAuditedObjectProperties();

            CreateMap<Cita.Dominio.Feriado, FeriadoDto>()
                .ForMember(x => x.InicioTexto, map => map.MapFrom(s => s.Inicio.ToString("dd/MM/yyyy")))
                .ForMember(x => x.FinTexto, map => map.MapFrom(s => s.Fin.ToString("dd/MM/yyyy")));
            CreateMap<CreateUpdateFeriadoDto, Feriado>()
                .ForMember(x => x.Id, map => map.Ignore());
        }
    }
}