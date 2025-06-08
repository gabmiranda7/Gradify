using Gradify.DTOs;
using Gradify.Services.Professores;
using Microsoft.AspNetCore.Mvc;

namespace Gradify.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorInterface _professorService;

        public ProfessorController(IProfessorInterface professorService)
        {
            _professorService = professorService;
        }

        public async Task<IActionResult> Index()
        {
            var professores = await _professorService.GetProfessores();
            return View(professores);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var professor = await _professorService.ObterPorId(id);
            if (professor == null)
                return NotFound();

            return View(professor);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(ProfessorDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _professorService.Criar(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var professor = await _professorService.ObterPorId(id);
            if (professor == null)
                return NotFound();

            return View(professor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ProfessorDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _professorService.Editar(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            await _professorService.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}