using Gradify.DTOs;
using Gradify.Services.Turma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gradify.Controllers
{
    public class TurmaController : Controller
    {
        private readonly ITurmaInterface _turmaService;

        public TurmaController(ITurmaInterface turmaService)
        {
            _turmaService = turmaService;
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public async Task<IActionResult> Index()
        {
            var turmas = await _turmaService.GetTurmas();
            return View(turmas);
        }

        [Authorize(Roles = "Administrador, Professor")]
        public IActionResult Criar()
        {
            var dto = new TurmaDto();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Criar(TurmaDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _turmaService.Criar(dto);
            TempData["Sucesso"] = "Turma criada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Editar(int id)
        {
            var turma = await _turmaService.ObterPorId(id);
            if (turma == null)
                return NotFound();

            ViewBag.TurmaId = id;
            return View(turma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Editar(int id, TurmaDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var turma = await _turmaService.Editar(id, dto);
            if (turma == null)
                return NotFound();

            TempData["Sucesso"] = "Turma editada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public async Task<IActionResult> Detalhes(int id)
        {
            var turma = await _turmaService.ObterPorId(id);
            if (turma == null)
                return NotFound();

            return View(turma);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            var sucesso = await _turmaService.Excluir(id);
            if (sucesso)
            {
                TempData["Sucesso"] = "Turma excluída com sucesso.";
            }
            else
            {
                TempData["Erro"] = "Erro ao excluir a turma.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}