using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaAgendamento.Models;

namespace AgendamentoSistema.Models
{
    public class Servico : BaseModel
    {
        

        [Required(ErrorMessage = "O nome do serviço é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do serviço deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A duração é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A duração deve ser um número positivo.")]
        public int DuracaoMinutos { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        // Relacionamento: Um serviço pode estar em vários agendamentos
        public virtual ICollection<Agendamento>? Agendamentos { get; set; }

        // Relacionamento: Um serviço pode ser oferecido por vários profissionais (opcional, dependendo da regra de negócio)
        // public virtual ICollection<ProfissionalServico>? ProfissionalServicos { get; set; }
    }
}
