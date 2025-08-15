using Microsoft.AspNetCore.Mvc;
using AgendamentoSistema.Data;
using AgendamentoSistema.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System; // Ensure System namespace is included for DateTime

namespace AgendamentoSistema.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgendamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agendamento (Admin View - List all appointments)
        public async Task<IActionResult> Index()
        {
            var agendamentos = await _context.Agendamentos
                                        .Include(a => a.Cliente) 
                                        .Include(a => a.Servico) 
                                        .Include(a => a.Profissional) 
                                        .OrderByDescending(a => a.DataHora) 
                                        .ToListAsync();
            return View(agendamentos);
        }

        // GET: Agendamento/SelecionarHorario?servicoId=5
        public async Task<IActionResult> SelecionarHorario(int servicoId)
        {
            var servico = await _context.Servicos.FindAsync(servicoId);
            if (servico == null)
            {
                return NotFound();
            }

            var horariosDisponiveis = await _context.Horarios
                                            .Include(h => h.Profissional) 
                                            .Where(h => h.Disponivel && h.DataHoraInicio > DateTime.Now)
                                            // Optional: Filter by professionals who offer the service if that logic exists
                                            .OrderBy(h => h.DataHoraInicio)
                                            .ToListAsync();

            ViewBag.Servico = servico;
            return View("SelecionarHorario", horariosDisponiveis); // Specify the view name
        }

        // GET: Agendamento/ConfirmarAgendamento?servicoId=5&horarioId=10&profissionalId=2
        public async Task<IActionResult> ConfirmarAgendamento(int servicoId, int horarioId, int profissionalId)
        {
            var servico = await _context.Servicos.FindAsync(servicoId);
            var horario = await _context.Horarios.Include(h => h.Profissional).FirstOrDefaultAsync(h => h.Id == horarioId);
            
            if (servico == null || horario == null || horario.ProfissionalId != profissionalId || !horario.Disponivel)
            {
                 // Add error message if horario is not found, not available, or professional doesn't match
                 TempData["ErroAgendamento"] = "O horário selecionado não está disponível ou é inválido.";
                 return RedirectToAction("SelecionarHorario", new { servicoId = servicoId });
            }

            // Simplified user handling: Assume user ID 1 exists or create it.
            // In a real app, get the logged-in user's ID.
            var usuarioId = 1; 
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                // Create a test user if user 1 doesn't exist
                usuario = new Usuario { Id = 1, Nome = "Cliente Teste", Email = "cliente@teste.com", Senha = "hashed_password" }; // Use password hashing!
                _context.Usuarios.Add(usuario);
                // Consider saving changes here or handle potential concurrency issues if multiple requests create user 1.
                // await _context.SaveChangesAsync(); 
            }

            var agendamentoViewModel = new Agendamento
            {
                UsuarioId = usuarioId,
                ServicoId = servicoId,
                ProfissionalId = profissionalId,
                DataHora = horario.DataHoraInicio, 
                Servico = servico,
                Profissional = horario.Profissional,
                Cliente = usuario 
            };

            ViewBag.HorarioId = horarioId; // Pass HorarioId for the POST form

            return View("ConfirmarAgendamento", agendamentoViewModel); // Specify the view name
        }

        // POST: Agendamento/ConfirmarAgendamento
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Bind parameters explicitly to prevent overposting
        public async Task<IActionResult> ConfirmarAgendamentoPost(int usuarioId, int servicoId, int profissionalId, int horarioId, DateTime dataHora)
        {
            // Re-validate if the horario is still available and matches the details
            var horario = await _context.Horarios.FindAsync(horarioId);
            if (horario == null || !horario.Disponivel || horario.DataHoraInicio != dataHora || horario.ProfissionalId != profissionalId)
            {
                TempData["ErroAgendamento"] = "O horário selecionado não está mais disponível ou os dados foram alterados. Por favor, tente novamente.";
                // Redirect back to selection, potentially passing servicoId if needed
                 return RedirectToAction("SelecionarHorario", new { servicoId = servicoId }); 
            }
             // Ensure the user exists (especially if not created/saved in the GET)
            var usuarioExists = await _context.Usuarios.AnyAsync(u => u.Id == usuarioId);
            if (!usuarioExists)
            {
                 // Handle case where user doesn't exist (e.g., create user 1 if it wasn't saved before)
                 if (usuarioId == 1) {
                     var usuario = new Usuario { Id = 1, Nome = "Cliente Teste", Email = "cliente@teste.com", Senha = "hashed_password" };
                     _context.Usuarios.Add(usuario);
                 } else {
                    TempData["ErroAgendamento"] = "Usuário não encontrado.";
                    return RedirectToAction("SelecionarHorario", new { servicoId = servicoId });
                 }
            }

            // Create the Agendamento entity
            var agendamento = new Agendamento
            {
                UsuarioId = usuarioId, 
                ServicoId = servicoId,
                ProfissionalId = profissionalId,
                DataHora = dataHora,
                Status = "Confirmado" // Default status
                // Observacoes can be added if there's a field in the form
            };

            // Mark the horario as unavailable
            horario.Disponivel = false;
            _context.Update(horario);

            _context.Agendamentos.Add(agendamento);

            try
            {
                await _context.SaveChangesAsync();
                 return RedirectToAction("AgendamentoConfirmado", new { id = agendamento.Id });
            }
            catch (DbUpdateException ex) 
            {
                // Log the error (using a logging framework is recommended)
                Console.WriteLine($"Error saving agendamento: {ex.Message}"); 
                TempData["ErroAgendamento"] = "Ocorreu um erro ao salvar o agendamento. Tente novamente.";
                // Consider redirecting to an error page or back to selection
                return RedirectToAction("SelecionarHorario", new { servicoId = servicoId });
            }
        }

        // GET: Agendamento/AgendamentoConfirmado/5
        public async Task<IActionResult> AgendamentoConfirmado(int id)
        {
            var agendamento = await _context.Agendamentos
                                        .Include(a => a.Servico)
                                        .Include(a => a.Profissional)
                                        .Include(a => a.Cliente)
                                        .FirstOrDefaultAsync(a => a.Id == id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return View("AgendamentoConfirmado", agendamento); // Specify the view name
        }
    }
}

