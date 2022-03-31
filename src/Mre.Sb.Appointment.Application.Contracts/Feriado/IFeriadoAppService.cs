using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Mre.Sb.Cita.Feriado
{
    public interface IFeriadoAppService : ICrudAppService<
        FeriadoDto,
            Guid,
            GetFeriadoInput,
            CreateUpdateFeriadoDto>
    {
        /// <summary>
        /// Valida si el día es feriado
        /// </summary>
        /// <param name="dia"></param>
        /// <returns></returns>
        public Task<bool> EsFeriado(DateTime dia);
    }
}
