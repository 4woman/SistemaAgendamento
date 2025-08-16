using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaAgendamento.Models;

namespace AgendamentoSistema.Models
{
    public class Horario : BaseModel
    {
        

        [Required(ErrorMessage = "A data e hora de início são obrigatórias.")]
        public DateTime DataHoraInicio { get; set; }

        [Required(ErrorMessage = "A data e hora de fim são obrigatórias.")]
        public DateTime DataHoraFim { get; set; }

        public bool Disponivel { get; set; } = true; // Por padrão, um horário cadastrado está disponível

        // Chave estrangeira para Profissional
        [Required(ErrorMessage = "O profissional é obrigatório.")]
        public int ProfissionalId { get; set; }

        [ForeignKey("ProfissionalId")]
        public virtual Profissional? Profissional { get; set; }
    }
}
