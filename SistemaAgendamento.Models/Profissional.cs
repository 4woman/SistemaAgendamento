using System.ComponentModel.DataAnnotations;
using SistemaAgendamento.Models;

namespace AgendamentoSistema.Models
{
    public class Profissional : BaseModel
    {
        

        [Required(ErrorMessage = "O nome do profissional é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do profissional deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        // Adicione outros campos relevantes, como especialidade, telefone, etc.
        public string? Especialidade { get; set; }

        // Relacionamento: Um profissional pode ter vários horários disponíveis
        public virtual ICollection<Horario>? HorariosDisponiveis { get; set; }

        // Relacionamento: Um profissional pode realizar vários agendamentos
        public virtual ICollection<Agendamento>? Agendamentos { get; set; }

        // Relacionamento: Um profissional pode oferecer vários serviços (se aplicável)
        // public virtual ICollection<ProfissionalServico>? ProfissionalServicos { get; set; }
    }
}
