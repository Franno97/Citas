using Microsoft.AspNetCore.Authorization;
using Mre.Sb.Cita.Permisos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Mre.Sb.Cita.Feriado
{
    [Authorize]
    public class FeriadoAppService :
        CrudAppService<
            Dominio.Feriado,
            FeriadoDto,
            Guid,
            GetFeriadoInput,
            CreateUpdateFeriadoDto>,
        IFeriadoAppService
    {
        private readonly GestionarFeriado _gestionarFeriado;

        public FeriadoAppService(IRepository<Dominio.Feriado, Guid> repository, GestionarFeriado gestionarFeriado)
            : base(repository)
        {
            _gestionarFeriado = gestionarFeriado;

            //Permisos
            GetPolicyName = CitaPermisos.Feriado.Default;
            GetListPolicyName = CitaPermisos.Feriado.Default;
            CreatePolicyName = CitaPermisos.Feriado.Create;
            UpdatePolicyName = CitaPermisos.Feriado.Update;
            DeletePolicyName = CitaPermisos.Feriado.Delete;
        }

        public override async Task<PagedResultDto<FeriadoDto>> GetListAsync(GetFeriadoInput entrada)
        {
            await CheckGetListPolicyAsync();

            var queryable = await CreateFilteredQueryAsync(entrada);
            queryable = queryable.Where(x => x.UnidadAdministrativaCalendarioId.Equals(entrada.UnidadAdministrativaCalendarioId));

            var totalRegistros = await AsyncExecuter.CountAsync(queryable);

            queryable = ApplySorting(queryable, entrada);
            queryable = ApplyPaging(queryable, entrada);

            var queryDto = queryable.Select(x => new FeriadoDto
            {
                Id = x.Id,
                Descripcion = x.Descripcion,
                Inicio = x.Inicio,
                InicioTexto = x.Inicio.ToString("dd/MM/yyyy"),
                Fin = x.Fin,
                FinTexto = x.Fin.ToString("dd/MM/yyyy")
            }).OrderBy(x => x.Inicio);

            var listaEntidadDto = await AsyncExecuter.ToListAsync(queryDto);

            return new PagedResultDto<FeriadoDto>(
                totalRegistros,
                listaEntidadDto
            );
        }

        public override async Task<FeriadoDto> CreateAsync(CreateUpdateFeriadoDto entrada)
        {
            await CheckCreatePolicyAsync();

            var entidad = await _gestionarFeriado.CrearAsync(descripcion: entrada.Descripcion, inicio: entrada.Inicio,
                fin: entrada.Fin, unidadAdministrativaCalendarioId: entrada.UnidadAdministrativaCalendarioId);

            TryToSetTenantId(entidad);

            await Repository.InsertAsync(entidad, autoSave: true);

            return await GetAsync(entidad.Id);
        }


        public async Task<bool> EsFeriado(DateTime dia)
        {

            var queryable = await Repository.GetQueryableAsync();
            dia = dia.Date;

            var existe = queryable.Any(x => x.Inicio.Date == dia || x.Fin.Date == dia);

            return existe;
        }
    }
}
