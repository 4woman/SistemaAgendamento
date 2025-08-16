using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaAgendamento.Models;

namespace AgendamentoSistema.Models
{
    public class Agendamento : BaseModel
    {
       

        [Required(ErrorMessage = "A data e hora do agendamento são obrigatórias.")]
        public DateTime DataHora { get; set; }

        // Chave estrangeira para Usuario (Cliente)
        [Required(ErrorMessage = "O cliente é obrigatório.")]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario? Cliente { get; set; }

        // Chave estrangeira para Servico
        [Required(ErrorMessage = "O serviço é obrigatório.")]
        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public virtual Servico? Servico { get; set; }

        // Chave estrangeira para Profissional
        [Required(ErrorMessage = "O profissional é obrigatório.")]
        public int ProfissionalId { get; set; }

        [ForeignKey("ProfissionalId")]
        public virtual Profissional? Profissional { get; set; }

        // Observações adicionais (opcional)
        public string? Observacoes { get; set; }

        // Status do agendamento (ex: Confirmado, Cancelado, Concluído)
        public StatusPedido Status { get; set; }
    }
}
