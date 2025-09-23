using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaAgendamento.Models;

namespace AgendamentoSistema.Models
{
    public class Agendamento : BaseModel
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int ProfissionalId { get; set; }
        public Profissional Profissional { get; set; }

        public int ServicoId { get; set; }
        public Servico Servico { get; set; }

        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }

        public StatusPedido Status { get; set; } // Agendado, Cancelado, Concluído





    }
}
