using System.ComponentModel.DataAnnotations;
using AgendamentoSistema.Models;

namespace SistemaAgendamento.Models
{
    public class Usuario : BaseModel
    {

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; } = string.Empty; // Em um sistema real, armazene o hash da senha

        // Relacionamento: Um usuário pode ter vários agendamentos
        public virtual ICollection<Agendamento>? Agendamentos { get; set; }

    }
}
