using AgendamentoSistema.Models;

namespace AgendamentoSistema.WebApi.DTO
{
    public class ServicoProfissionalDTO
    {
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }

        public int ProfissionalId { get; set; }
        public Profissional Profissional { get; set; }

    }
}
