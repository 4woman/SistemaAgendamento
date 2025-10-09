using System.ComponentModel.DataAnnotations;
using SistemaAgendamento.Models;

namespace AgendamentoSistema.Models
{
    public class Profissional : BaseModel
    {

        public string Nome { get; set; } = string.Empty;

       public ICollection<ServicoProfissional> ServicoProfissionals { get; set; }
       public ICollection<HorarioDisponivel> Horarios { get; set; }
       public ICollection<Agendamento> Agendamentos { get; set; }




        
    }
}
