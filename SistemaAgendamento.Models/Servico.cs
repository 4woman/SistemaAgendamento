using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaAgendamento.Models;

namespace AgendamentoSistema.Models
{
    public class Servico : BaseModel
    {
        

       
        public string NomeServico { get; set; }
        public string Descricao { get; set; } 
        public int DuracaoMinutos { get; set; }
        public decimal Preco { get; set; }

        public ICollection<ServicoProfissional> Profissionais { get; set; }
        public ICollection<Agendamento> Agendamentos { get; set; }
    }
}
