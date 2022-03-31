using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mre.Sb.Appointment;
using Xunit;

namespace Mre.Sb.Cita.Cita
{
    public class DisponibilidadAppService_Tests : AppointmentApplicationTestBase
    {
        private readonly IDisponibilidadAppService _disponibilidadAppService;

        public DisponibilidadAppService_Tests(IDisponibilidadAppService disponibilidadAppService)
        {
            _disponibilidadAppService = disponibilidadAppService;
        }

        [Fact]
        public async Task ObtenerPeriodosDisponibles()
        {
            List<PeriodoDisponibleDto> result = await _disponibilidadAppService
                .ObtenerPeriodosDisponibles(new ObtenerDisponibilidadEntrada
                {
                    Fecha = new DateTime(), 
                    ServicioId = Guid.NewGuid(),
                    UnidadAdministrativaId = Guid.NewGuid()
                });

            Assert.True(result.Any());
        }
    }
}