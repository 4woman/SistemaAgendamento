using System.ComponentModel.DataAnnotations;

namespace AgendamentoSistema.WebApi.DTO
{
    public class HorarioDisponivelCreateDTO
    {
        [Required]
        public DateTime DataHoraInicio { get; set; }

        [Required]
        public DateTime DataHoraFim { get; set; }

        [Required]
        public int ProfissionalId { get; set; }
    }
}
