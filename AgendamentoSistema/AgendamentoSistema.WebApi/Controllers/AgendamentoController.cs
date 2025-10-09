using AgendamentoSistema.Models;
using AgendamentoSistema.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using SistemaAgendamento.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendamentoSistema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly AgendamentoService _service = new();
        private readonly ProfissionalService _profissionalService = new();
        private readonly ServicoService _servicoService = new();
        private readonly UsuarioService _usuarioService = new();


        
        [HttpPost]
        public IActionResult Agendar([FromBody] AgendamentoDTO dto)
        {
            var model = new Agendamento
            {
                UsuarioId = dto.UsuarioId,
                Usuario = _usuarioService.ListarPorId(dto.UsuarioId),
                ProfissionalId = dto.ProfissionalId,
                Profissional = _profissionalService.ListarPorId(dto.Profissional.Id),
                ServicoId = dto.ServicoId,
                Servico = _servicoService.ListarPorId(dto.ServicoId),
                DataHoraInicio = dto.DataHoraInicio,
                DataHoraFim = dto.DataHoraFim,
                Status = dto.Status,
            };

            var id = _service.Cadastrar(model);
            return Ok(id);
        }

        // GET: api/agendamento
        [HttpGet]
        public IActionResult Listar()
        {
            var listar = _service.Listar();
            var resultado = listar.Select(sp => new AgendamentoDTO
            {
                UsuarioId = sp.UsuarioId,
                Usuario = _usuarioService.ListarPorId(sp.UsuarioId),
                ServicoId = sp.ServicoId,
                Servico = _servicoService.ListarPorId(sp.ServicoId),
                ProfissionalId = sp.ProfissionalId,
                Profissional = _profissionalService.ListarPorId(sp.ProfissionalId)
            });
            
            return Ok(resultado);
        }

        // GET: api/agendamento/5
        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            var agendamento = _service.ListarPorId(id);
            if (agendamento == null)
                return NotFound();
            return Ok(agendamento);
        }

        // PUT: api/agendamento/5
        [HttpPut("{id}")]
        public IActionResult Editar(int id, [FromBody] Agendamento model)
        {
            model.Id = id;
            var sucesso = _service.Editar(model);
            if (!sucesso)
                return NotFound();
            return Ok(new { Mensagem = "Agendamento atualizado com sucesso!" });
        }

        // DELETE: api/agendamento/5
        [HttpDelete("{id}")]
        public IActionResult Cancelar(int id)
        {
            var sucesso = _service.CancelarAgendamento(id);
            if (!sucesso)
                return NotFound();
            return Ok(new { Mensagem = "Agendamento cancelado com sucesso!" });
        }

        // GET: api/agendamento/usuario/3
        [HttpGet("usuario/{usuarioId}")]
        public IActionResult ListarPorUsuario(int usuarioId)
        {
            var lista = _service.ListarPorUsuario(usuarioId);
            return Ok(lista);
        }

        // GET: api/agendamento/profissional/2
        [HttpGet("profissional/{profissionalId}")]
        public IActionResult ListarPorProfissional(int profissionalId)
        {
            var lista = _service.ListarPorProfissional(profissionalId);
            return Ok(lista);
        }
    }
}
