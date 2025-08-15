using AgendamentoSistema.Data;
using AgendamentoSistema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoSistema.Controllers
{
    public class ProfissionalController : Controller
    {
        readonly private ApplicationDbContext _db;
        public ProfissionalController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Profissional> profissionais = _db.Profissionais;
            return View(profissionais);
        }

        //viwew para criar um novo profissional
        public IActionResult Cadastrar()
        {

            return View();
        }

        // POST: Servicos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar([Bind("Nome,Especialidade,HorariosDisponiveis,Agendamentos")] Profissional profissional)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Agendamentos");

            if (ModelState.IsValid)
            {
                _db.Add(profissional);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profissional);
        }
    }
}
