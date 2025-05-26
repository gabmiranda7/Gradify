using Gradify.DTOs;
using Gradify.Models;
using Gradify.Services;
using Gradify.Services.Professores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gradify.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorInterface _professorService;

        public ProfessorController(IProfessorInterface professorService)
        {
            _professorService = professorService;
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public async Task<IActionResult> Index()
        {
            var professores = await _professorService.GetProfessores();
            return View(professores);
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var professor = await _professorService.ObterPorId(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        [Authorize(Roles = "Administrador, Professor")]
        public IActionResult Criar()
        {
            return View(new ProfessorDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Criar(ProfessorDto dto)
        {
            if (ModelState.IsValid)
            {
                await _professorService.Criar(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Editar(int id)
        {
            var professor = await _professorService.ObterPorId(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Editar(int id, ProfessorDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var professor = await _professorService.Editar(id, dto);
                if (professor == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Excluir(int id)
        {
            var success = await _professorService.Excluir(id);
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