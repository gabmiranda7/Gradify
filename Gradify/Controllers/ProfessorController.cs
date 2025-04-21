using Gradify.Models;
using Gradify.Services;
using Microsoft.AspNetCore.Mvc;
using Gradify.Dto;
using Gradify.Services.Professor;
using Microsoft.AspNetCore.Authorization;

namespace Gradify.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorInterface _professorService;

        public ProfessorController(IProfessorInterface professorService)
        {
            _professorService = professorService;
        }

        [Authorize(Roles = "Aluno, Professor")]
        public IActionResult Index()
        {
            var professores = _professorService.GetProfessores();
            return View(professores);
        }

        [Authorize(Roles = "Aluno, Professor")]
        public IActionResult Detalhes(int id)
        {
            var professor = _professorService.ObterPorId(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        [Authorize(Roles = "Professor")]
        public IActionResult Criar()
        {
            return View(new ProfessorCriacaoDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Professor")]
        public IActionResult Criar(ProfessorCriacaoDto dto)
        {
            if (ModelState.IsValid)
            {
                var professor = _professorService.Criar(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        [Authorize(Roles = "Professor")]
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
        [Authorize(Roles = "Professor")]
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

        [Authorize(Roles = "Professor")]
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