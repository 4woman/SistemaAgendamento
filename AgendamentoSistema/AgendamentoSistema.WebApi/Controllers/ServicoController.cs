using AgendamentoSistema.Models;
using AgendamentoSistema.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using SistemaAgendamento.Models;
using SistemaAgendamento.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendamentoSistema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly BaseService<Servico> _service = new();

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ServicoDTO dto)
        {
            Servico servico = new Servico();
            {
                servico.NomeServico = dto.NomeServico;
                servico.Descricao = dto.Descricao;
                servico.DuracaoMinutos = dto.DuracaoMinutos;
                servico.Preco = dto.Preco;


                servico.Agendamentos = new List<Agendamento>();
                servico.Profissionais = new List<ServicoProfissional>();
                
            }

            var id = _service.Cadastrar(servico);
            return Ok(new { Id = id, Mensagem = "Serviço cadastrado com sucesso!" });
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            var servico = _service.ListarPorId(id);
            if (servico == null) return NotFound();
            return Ok(servico);
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Servico model)
        {
            model.Id = id;
            var sucesso = _service.Editar(model);
            return sucesso ? Ok("Serviço atualizado!") : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var sucesso = _service.Deletar(id);
            return sucesso ? Ok("Serviço removido!") : NotFound();
        }
    }
}
