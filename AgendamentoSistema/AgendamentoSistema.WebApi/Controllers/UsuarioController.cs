using System.ComponentModel.DataAnnotations;
using AgendamentoSistema.Models;
using AgendamentoSistema.WebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaAgendamento.Models;
using SistemaAgendamento.Service;

namespace AgendamentoSistema.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly BaseService<Usuario> _service = new();


        [HttpPost]
        public IActionResult Cadastrar([FromBody] UsuarioDTO dto)
        {
            Usuario model = new Usuario();
            {
                model.Nome = dto.Nome;
                model.Email = dto.Email;
                model.Telefone = dto.Telefone;
                model.Agendamentos = new List<Agendamento>();
            }

            var id = _service.Cadastrar(model);
            return Ok(new { Id = id, Mensagem = "Usuário cadastrado com sucesso!" });
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            var usuario = _service.ListarPorId(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Usuario model)
        {
            model.Id = id;
            var sucesso = _service.Editar(model);
            return sucesso ? Ok("Usuário atualizado!") : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var sucesso = _service.Deletar(id);
            return sucesso ? Ok("Usuário removido!") : NotFound();
        }
    }
}
