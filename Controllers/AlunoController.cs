using Gradify.DTOs;
using Gradify.Services.Alunos;
using Microsoft.AspNetCore.Mvc;

namespace Gradify.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoInterface _alunoInterface;

        public AlunoController(IAlunoInterface alunoService)
        {
            _alunoInterface = alunoService;
        }

        public async Task<IActionResult> Index()
        {
            var alunos = await _alunoInterface.GetAlunos();
            return View(alunos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(AlunoDto alunoDto)
        {
            if (!ModelState.IsValid) return View(alunoDto);

            try
            {
                await _alunoInterface.Criar(alunoDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao criar aluno: {ex.Message}");
                return View(alunoDto);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var aluno = await _alunoInterface.ObterPorId(id);
            if (aluno == null) return NotFound();
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AlunoDto alunoDto)
        {
            if (!ModelState.IsValid) return View(alunoDto);

            try
            {
                var atualizado = await _alunoInterface.Editar(id, alunoDto);
                if (atualizado == null) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao editar aluno: {ex.Message}");
                return View(alunoDto);
            }
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var aluno = await _alunoInterface.ObterPorId(id);
            if (aluno == null) return NotFound();
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var sucesso = await _alunoInterface.Excluir(id);
                if (!sucesso) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao excluir aluno: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}