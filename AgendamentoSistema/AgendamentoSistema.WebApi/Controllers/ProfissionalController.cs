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
    public class ProfissionalController : ControllerBase
    {
        private readonly BaseService<Profissional> _service = new();

        [HttpPost]
        public IActionResult Cadastrar([FromBody] ProfissionalDTO dto)
        {
            Profissional profissional = new Profissional();
            {
                profissional.Nome = dto.Nome;
                profissional.Agendamentos = new List<Agendamento>();
                profissional.ServicoProfissionals = new List<ServicoProfissional>();
                profissional.Horarios = new List<HorarioDisponivel>();
            }
            var id = _service.Cadastrar(profissional);
            return Ok(new { Id = id, Mensagem = "Profissional cadastrado com sucesso!" });
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            var profissional = _service.ListarPorId(id);
            if (profissional == null) return NotFound();
            return Ok(profissional);
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Profissional model)
        {
            model.Id = id;
            var sucesso = _service.Editar(model);
            return sucesso ? Ok("Profissional atualizado!") : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var sucesso = _service.Deletar(id);
            return sucesso ? Ok("Profissional removido!") : NotFound();
        }
    }
}
