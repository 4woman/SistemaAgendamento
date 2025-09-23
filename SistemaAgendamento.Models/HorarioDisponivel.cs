using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SistemaAgendamento.Models;

namespace AgendamentoSistema.Models
{
    public class HorarioDisponivel : BaseModel
    {
        
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public int ProfissionalId { get; set; }
        public Profissional Profissional { get; set; }

    }
}
