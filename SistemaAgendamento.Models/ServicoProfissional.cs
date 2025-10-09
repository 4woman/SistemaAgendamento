using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendamentoSistema.Models;

namespace SistemaAgendamento.Models
{
    public class ServicoProfissional: BaseModel
    {
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }

       public int ProfissionalId { get; set; }
       public Profissional Profissional { get; set; }
    }
}
