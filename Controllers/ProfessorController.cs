using Gradify.DTOs;
using Gradify.Services.Professores;
using Microsoft.AspNetCore.Mvc;

namespace Gradify.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorInterface _professorInterface;

        public ProfessorController(IProfessorInterface professorService)
        {
            _professorInterface = professorService;
        }

        public async Task<IActionResult> Index()
        {
            var professores = await _professorInterface.GetProfessores();
            return View(professores);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(ProfessorDto professorDto)
        {
            if (!ModelState.IsValid) return View(professorDto);

            try
            {
                await _professorInterface.Criar(professorDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao criar professor: {ex.Message}");
                return View(professorDto);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var professor = await _professorInterface.ObterPorId(id);
            if (professor == null) return NotFound();
            return View(professor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, ProfessorDto professorDto)
        {
            if (!ModelState.IsValid) return View(professorDto);

            try
            {
                var atualizado = await _professorInterface.Editar(id, professorDto);
                if (atualizado == null) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao editar professor: {ex.Message}");
                return View(professorDto);
            }
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var professor = await _professorInterface.ObterPorId(id);
            if (professor == null) return NotFound();
            return View(professor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var sucesso = await _professorInterface.Excluir(id);
                if (!sucesso) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao excluir professor: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}