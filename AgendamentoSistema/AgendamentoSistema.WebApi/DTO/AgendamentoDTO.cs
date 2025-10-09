using AgendamentoSistema.Models;
using SistemaAgendamento.Models;

namespace AgendamentoSistema.WebApi.DTO
{
    public class AgendamentoDTO
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int ProfissionalId { get; set; }
        public Profissional Profissional { get; set; }

        public int ServicoId { get; set; }
        public Servico Servico { get; set; }

        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        public StatusPedido Status { get; set; }
    }
}
