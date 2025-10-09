using AgendamentoSistema.Models;

namespace AgendamentoSistema.WebApi.DTO
{
    public class HorarioDisponivelDTO
    {
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public int ProfissionalId { get; set; }
        public Profissional Profissional { get; set; }
    }
}
