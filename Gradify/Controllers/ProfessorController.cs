using Gradify.Models;
using Gradify.Services;
using Microsoft.AspNetCore.Mvc;
using Gradify.Dto;
using Gradify.Services.Professor;

namespace Gradify.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorInterface _professorService;

        public ProfessorController(IProfessorInterface professorService)
        {
            _professorService = professorService;
        }

        public IActionResult Index()
        {
            var professores = _professorService.GetProfessores();
            return View(professores);
        }

        public IActionResult Detalhes(int id)
        {
            var professor = _professorService.ObterPorId(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        public IActionResult Criar()
        {
            return View(new ProfessorCriacaoDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(ProfessorCriacaoDto dto)
        {
            if (ModelState.IsValid)
            {
                var professor = _professorService.Criar(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public IActionResult Editar(int id)
        {
            var professor = _professorService.ObterPorId(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, ProfessorCriacaoDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var professor = _professorService.Editar(id, dto);
                if (professor == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public IActionResult Excluir(int id)
        {
            var success = _professorService.Excluir(id);
            if (success)
            {
                TempData["Sucesso"] = "Professor excluído com sucesso!";
            }
            else
            {
                TempData["Erro"] = "Erro ao excluir o professor.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}