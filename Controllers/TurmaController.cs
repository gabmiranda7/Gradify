using Gradify.DTOs;
using Gradify.Services.Turmas;
using Microsoft.AspNetCore.Mvc;

namespace Gradify.Controllers
{
    public class TurmaController : Controller
    {
        private readonly ITurmaInterface _turmaInterface;

        public TurmaController(ITurmaInterface turmaInterface)
        {
            _turmaInterface = turmaInterface;
        }

        public async Task<IActionResult> Index()
        {
            var turmas = await _turmaInterface.GetTurmas();
            return View(turmas);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(TurmaDto turmaDto)
        {
            if (!ModelState.IsValid) return View(turmaDto);

            try
            {
                await _turmaInterface.Criar(turmaDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao criar turma: {ex.Message}");
                return View(turmaDto);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var turma = await _turmaInterface.ObterPorId(id);
            if (turma == null) return NotFound();
            return View(turma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, TurmaDto turmaDto)
        {
            if (!ModelState.IsValid) return View(turmaDto);

            try
            {
                var atualizado = await _turmaInterface.Editar(id, turmaDto);
                if (atualizado == null) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao editar turma: {ex.Message}");
                return View(turmaDto);
            }
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var turma = await _turmaInterface.ObterPorId(id);
            if (turma == null) return NotFound();
            return View(turma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var sucesso = await _turmaInterface.Excluir(id);
                if (!sucesso) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao excluir turma: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}