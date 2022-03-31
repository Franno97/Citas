using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Newtonsoft.Json;
using Mre.Sb.Cita.Dominio;
using System;
using Mre.Sb.Cita.Permisos;

namespace Mre.Sb.Cita.Cita
{
    public class ServicioCalendarioAppService :
        CrudAppService<
            ServicioCalendario,
            ServicioCalendarioDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CrearActualizarServicioCalendarioDto>,
        IServicioCalendarioAppService
    {
        private readonly GestionarServicioCalendario _gestionarServicioCalendario;

        public ServicioCalendarioAppService(IRepository<ServicioCalendario, Guid> repositorio,
            GestionarServicioCalendario gestionarServicioCalendario)
            : base(repositorio)
        {
            _gestionarServicioCalendario = gestionarServicioCalendario;

            //Permisos
            //GetPolicyName = CitaPermisos.ServicioCalendario.Default;
            //GetListPolicyName = CitaPermisos.ServicioCalendario.Default;
            CreatePolicyName = CitaPermisos.ServicioCalendario.Create;
            UpdatePolicyName = CitaPermisos.ServicioCalendario.Update;
            DeletePolicyName = CitaPermisos.ServicioCalendario.Delete;
        }

        public override async Task<ServicioCalendarioDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var queryable = await Repository.GetQueryableAsync();
            queryable = queryable.Where(a => a.Id == id);

            var queryableDto = queryable.Select(entidad => new ServicioCalendarioDto
            {
                Id = entidad.Id,
                UnidadAdministrativaId = entidad.UnidadAdministrativaId,
                ServicioId = entidad.ServicioId,
                PlanTrabajo = JsonConvert.DeserializeObject<PlanSemanal>(entidad.PlanTrabajo),
                Duracion = entidad.Duracion,
                InicioAgendamiento = entidad.InicioAgendamiento,
                FinAgendamiento = entidad.FinAgendamiento,
                DiasDisponibilidad = entidad.DiasDisponibilidad,
                CitaAutomatica = entidad.CitaAutomatica,
                HorasGracia = entidad.HorasGracia,
                UsarVentanillas = entidad.UsarVentanillas
            });

            var entidadDto = await AsyncExecuter.FirstOrDefaultAsync(queryableDto);

            return entidadDto;
        }

        public async Task<ListResultDto<ServicioCalendarioLookupDto>> GetLookupAsync()
        {
            await CheckGetListPolicyAsync();

            var list = await Repository.GetListAsync();

            return new ListResultDto<ServicioCalendarioLookupDto>(
                ObjectMapper.Map<List<ServicioCalendario>, List<ServicioCalendarioLookupDto>>(list)
            );
        }

        public override async Task<ServicioCalendarioDto> CreateAsync(CrearActualizarServicioCalendarioDto entrada)
        {
            await CheckCreatePolicyAsync();

            var entidad = await _gestionarServicioCalendario.CreateAsync(
                unidadAdministrativaId: entrada.UnidadAdministrativaId,
                servicioId: entrada.ServicioId,
                planTrabajo: JsonConvert.SerializeObject(entrada.PlanTrabajo),
                duracion: entrada.Duracion,
                inicioAgendamiento: entrada.InicioAgendamiento,
                finAgendamiento: entrada.FinAgendamiento,
                diasDisponibilidad: entrada.DiasDisponibilidad,
                citaAutomatica: entrada.CitaAutomatica,
                horasGracia: entrada.HorasGracia,
                usarVentanillas: entrada.UsarVentanillas
            );

            TryToSetTenantId(entidad);

            await Repository.InsertAsync(entidad, autoSave: true);

            return await GetAsync(entidad.Id);
        }

        public async Task<List<ServicioCalendarioDto>> ObtenerPorServicioUnidadAdministrativa(
            ObtenerServicioCalendarioEntrada entrada)
        {

            var queryable = await Repository.GetQueryableAsync();

            queryable = queryable.Where(x => x.UnidadAdministrativaId == entrada.UnidadAdministrativaId
                                             && x.ServicioId == entrada.ServicioId);

            queryable = queryable.WhereIf(entrada.Fecha != null,
                x => entrada.Fecha >= x.InicioAgendamiento
                     && entrada.Fecha <= x.FinAgendamiento);

            var queryableDto = queryable.Select(entidad => new ServicioCalendarioDto()
            {
                Id = entidad.Id,
                UnidadAdministrativaId = entidad.UnidadAdministrativaId,
                ServicioId = entidad.ServicioId,
                PlanTrabajo = JsonConvert.DeserializeObject<PlanSemanal>(entidad.PlanTrabajo),
                Duracion = entidad.Duracion,
                InicioAgendamiento = entidad.InicioAgendamiento,
                FinAgendamiento = entidad.FinAgendamiento,
                DiasDisponibilidad = entidad.DiasDisponibilidad,
                CitaAutomatica = entidad.CitaAutomatica,
                HorasGracia = entidad.HorasGracia,
                UsarVentanillas = entidad.UsarVentanillas
            }).OrderBy(x => x.InicioAgendamiento);

            var entityDtos = await AsyncExecuter.ToListAsync(queryableDto);

            return entityDtos;
        }

        public async Task<bool> ExistePorServicioUnidadAdministrativa(ObtenerServicioCalendarioEntrada entrada)
        {
            await CheckGetListPolicyAsync();
            var queryable = await Repository.GetQueryableAsync();

            var existe = queryable.Any(x => x.UnidadAdministrativaId == entrada.UnidadAdministrativaId && x.ServicioId == entrada.ServicioId);

            return existe;
        }
    }
}