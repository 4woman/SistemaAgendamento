using AgendamentoSistema.Models;
using AgendamentoSistema.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using SistemaAgendamento.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendamentoSistema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioDisponivelController : ControllerBase
    {
        private readonly HorarioDisponivelService _horarioService = new();
        private readonly BaseService<Profissional> _profissional = new(); 

        // Cadastrar novo horário
        [HttpPost]
        public IActionResult Cadastrar([FromBody] HorarioDisponivelCreateDTO dto)
        {
            var model = new HorarioDisponivel
            {
                DataHoraInicio = dto.DataHoraInicio,
                DataHoraFim = dto.DataHoraFim,
                ProfissionalId = dto.ProfissionalId,
                Profissional = _profissional.ListarPorId(dto.ProfissionalId),
            };

            var id = _horarioService.Cadastrar(model);
            return Ok(new { Id = id, Mensagem = "Horário cadastrado com sucesso!" });
        }

        // Listar todos os horários de um profissional
        [HttpGet]
        public IActionResult Listar()
        {
            var lista = _horarioService.Listar();

            var resultado = lista.Select(p => new HorarioDisponivelDTO
            {
                DataHoraInicio = p.DataHoraInicio,
                DataHoraFim = p.DataHoraFim,
                ProfissionalId = p.ProfissionalId,
                Profissional = _profissional.ListarPorId(p.ProfissionalId)
            });
            return Ok(resultado);
        }

        // Listar apenas horários disponíveis (sem agendamento)
        
    }
}
