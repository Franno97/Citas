using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Mre.Sb.Cita.Cita
{
    public interface ICitaAppService :
        ICrudAppService<CitaDto,
            Guid,
            GetCitaInput,
            CreateUpdateCitaDto>
    {

        /// <summary>
        /// Si existe una cita agendada del servicio de la persona
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<bool> ExisteCitaAgendada(CreateUpdateCitaDto entrada);

        /// <summary>
        /// Obtiene las citas por un servicio
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        public Task<List<CitaDto>> ObtenerPorServicioUnidadAdministrativa(ObtenerCitaEntrada entrada);

        /// <summary>
        /// Reagendar la cita
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entrada"></param>
        /// <returns></returns>
        public Task<CitaDto> Reagendar(ReagendarCitaDto entrada);

        /// <summary>
        /// Cancelar la cita
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entrada"></param>
        /// <returns></returns>
        public Task<CitaDto> Cancelar(Guid id);

        /// <summary>
        /// Agendar una cita por la persona
        /// </summary>
        /// <param name="entrada"></param>
        /// <returns></returns>
        public Task<CrearCitaSalida> Agendar(CreateUpdateCitaDto entrada);
    }
}