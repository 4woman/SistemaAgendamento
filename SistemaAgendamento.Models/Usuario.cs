using System.ComponentModel.DataAnnotations;
using AgendamentoSistema.Models;

namespace SistemaAgendamento.Models
{
    public class Usuario : BaseModel
    {

        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty; 

        public ICollection<Agendamento> Agendamentos { get; set; }

    }
}
