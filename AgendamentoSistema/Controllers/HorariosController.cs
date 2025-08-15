using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgendamentoSistema.Data;
using AgendamentoSistema.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AgendamentoSistema.Controllers
{
    public class HorariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HorariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Horarios
        public async Task<IActionResult> Index()
        {
            // Listar todos os horários ordenados por data/hora e incluindo o profissional
            var horarios = await _context.Horarios
                                     .Include(h => h.Profissional)
                                     .OrderBy(h => h.DataHoraInicio)
                                     .ToListAsync();
            return View(horarios);
        }

        // GET: Horarios/Create
        public IActionResult Create()
        {
            // Passar a lista de profissionais para a View
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "Id", "Nome");
            return View();
        }

        // POST: Horarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataHoraInicio,DataHoraFim,ProfissionalId")] Horario horario)
        {
            // Remover validações desnecessárias
            ModelState.Remove("Id");
            ModelState.Remove("Profissional"); // A entidade Profissional não é preenchida diretamente

            // Validar se DataHoraFim é posterior a DataHoraInicio
            if (horario.DataHoraFim <= horario.DataHoraInicio)
            {
                ModelState.AddModelError("DataHoraFim", "A data/hora de fim deve ser posterior à data/hora de início.");
            }

            // Validar sobreposição de horários para o mesmo profissional (simplificado)
            var sobreposicao = await _context.Horarios
                .AnyAsync(h => h.ProfissionalId == horario.ProfissionalId &&
                               h.DataHoraInicio < horario.DataHoraFim &&
                               h.DataHoraFim > horario.DataHoraInicio);

            if (sobreposicao)
            {
                 ModelState.AddModelError(string.Empty, "Este horário entra em conflito com outro horário já cadastrado para o mesmo profissional.");
            }


            if (ModelState.IsValid)
            {
                horario.Disponivel = true; // Horário criado é sempre disponível inicialmente
                _context.Add(horario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "Id", "Nome", horario.ProfissionalId);
            return View(horario);
        }

        // GET: Horarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "Id", "Nome", horario.ProfissionalId);
            return View(horario);
        }

        // POST: Horarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHoraInicio,DataHoraFim,Disponivel,ProfissionalId")] Horario horario)
        {
            if (id != horario.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Profissional");

            // Validar se DataHoraFim é posterior a DataHoraInicio
            if (horario.DataHoraFim <= horario.DataHoraInicio)
            {
                ModelState.AddModelError("DataHoraFim", "A data/hora de fim deve ser posterior à data/hora de início.");
            }

             // Validar sobreposição de horários para o mesmo profissional (excluindo o próprio horário)
            var sobreposicao = await _context.Horarios
                .AnyAsync(h => h.Id != horario.Id && // Exclui o próprio horário da verificação
                               h.ProfissionalId == horario.ProfissionalId &&
                               h.DataHoraInicio < horario.DataHoraFim &&
                               h.DataHoraFim > horario.DataHoraInicio);

            if (sobreposicao)
            {
                 ModelState.AddModelError(string.Empty, "Este horário entra em conflito com outro horário já cadastrado para o mesmo profissional.");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioExists(horario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "Id", "Nome", horario.ProfissionalId);
            return View(horario);
        }

        // GET: Horarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario = await _context.Horarios
                .Include(h => h.Profissional)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horario == null)
            {
                return NotFound();
            }

            // Verificar se o horário está associado a algum agendamento
            var agendamentoAssociado = await _context.Agendamentos.AnyAsync(a => a.DataHora == horario.DataHoraInicio && a.ProfissionalId == horario.ProfissionalId);
            if (agendamentoAssociado)
            {
                 ViewData["ErroExclusao"] = "Não é possível excluir este horário pois ele está vinculado a um agendamento existente.";
            }
            // Verificar se o horário já passou (não permitir exclusão de histórico? Opcional)
            // if (horario.DataHoraFim < DateTime.Now)
            // {
            //     ViewData["ErroExclusao"] = "Não é possível excluir um horário que já ocorreu.";
            // }


            return View(horario);
        }

        // POST: Horarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
             if (horario == null)
            {
                return NotFound();
            }

            // Re-verificar se o horário está associado a algum agendamento antes de excluir
            var agendamentoAssociado = await _context.Agendamentos.AnyAsync(a => a.DataHora == horario.DataHoraInicio && a.ProfissionalId == horario.ProfissionalId);
            if (agendamentoAssociado)
            {
                // Adicionar erro e retornar para a view de confirmação
                ModelState.AddModelError(string.Empty, "Não é possível excluir este horário pois ele está vinculado a um agendamento existente.");
                // Recarregar dados necessários para a view
                horario = await _context.Horarios.Include(h => h.Profissional).FirstOrDefaultAsync(m => m.Id == id);
                ViewData["ErroExclusao"] = "Não é possível excluir este horário pois ele está vinculado a um agendamento existente."; // Passar a mensagem de erro novamente se necessário
                return View(horario);
            }

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioExists(int id)
        {
            return _context.Horarios.Any(e => e.Id == id);
        }
    }
}
