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
    public class ServicoProfissionalController : ControllerBase
    {
        private readonly BaseService<ServicoProfissional> _serviceProfissional = new();
        private readonly BaseService<Servico> _service = new();
        private readonly BaseService<Profissional> _profissional = new();


        [HttpPost]
        public IActionResult Cadastrar([FromBody] ServicoProfissionalCreateDTO dto)
        {
            var entidade = new ServicoProfissional
            {
                ServicoId = dto.ServicoId,
                Servico = _service.ListarPorId(dto.ServicoId),
                ProfissionalId = dto.ProfissionalId,
                Profissional = _profissional.ListarPorId(dto.ProfissionalId)
            };

            var id = _serviceProfissional.Cadastrar(entidade);
            return Ok(new { Id = id, Mensagem = "Vínculo cadastrado com sucesso!" });
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var lista = _serviceProfissional.Listar();
            var resultado = lista.Select(sp => new ServicoProfissionalDTO
            {
                ServicoId = sp.ServicoId,
                Servico = _service.ListarPorId(sp.ServicoId),
                ProfissionalId = sp.ProfissionalId,
                Profissional = _profissional.ListarPorId(sp.ProfissionalId)
            });
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            var sp = _serviceProfissional.ListarPorId(id);
            if (sp == null) return NotFound();

            var dto = new ServicoProfissionalDTO
            {
                ServicoId = sp.ServicoId,
                Servico = _service.ListarPorId(sp.ServicoId),
                ProfissionalId = sp.ProfissionalId,
                Profissional = _profissional.ListarPorId(sp.ProfissionalId)
            };

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public IActionResult Editar(int servicoId, int profissionalId, [FromBody] ServicoProfissional model)
        {
            model.ServicoId = servicoId;
            model.ProfissionalId = profissionalId;
            var sucesso = _serviceProfissional.Editar(model);
            return sucesso ? Ok("Vínculo atualizado!") : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var sucesso = _serviceProfissional.Deletar(id);
            return sucesso ? Ok("Vínculo removido!") : NotFound();
        }
    }
}
