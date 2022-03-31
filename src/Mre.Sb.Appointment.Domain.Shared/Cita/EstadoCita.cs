using System.ComponentModel.DataAnnotations;

namespace Mre.Sb.Cita
{
    public enum EstadoCita
    {
        [Display(Name = "Registrado")]
        Registrado = 1,

        [Display(Name = "Atendido")]
        Atendido = 2,

        [Display(Name = "Cancelado")]
        Cancelado = 3,

        [Display(Name = "Caducado")]
        Caducado = 4,
    }
}
