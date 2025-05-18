using Gradify.DTOs;
using Gradify.Services.Anotacoes;
using Microsoft.AspNetCore.Mvc;

namespace Gradify.Controllers
{
    public class AnotacaoController : Controller
    {
        private readonly IAnotacaoInterface _anotacaoInterface;

        public AnotacaoController(IAnotacaoInterface anotacaoService)
        {
            _anotacaoInterface = anotacaoService;
        }

        public async Task<IActionResult> Index()
        {
            var anotacoes = await _anotacaoInterface.GetAnotacoes();
            return View(anotacoes);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(AnotacaoDto anotacaoDto)
        {
            if (!ModelState.IsValid) return View(anotacaoDto);

            try
            {
                await _anotacaoInterface.Criar(anotacaoDto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao criar anotação: {ex.Message}");
                return View(anotacaoDto);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var anotacao = await _anotacaoInterface.ObterPorId(id);
            if (anotacao == null) return NotFound();
            return View(anotacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AnotacaoDto anotacaoDto)
        {
            if (!ModelState.IsValid) return View(anotacaoDto);

            try
            {
                var atualizado = await _anotacaoInterface.Editar(id, anotacaoDto);
                if (atualizado == null) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao editar anotação: {ex.Message}");
                return View(anotacaoDto);
            }
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var anotacao = await _anotacaoInterface.ObterPorId(id);
            if (anotacao == null) return NotFound();
            return View(anotacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var sucesso = await _anotacaoInterface.Excluir(id);
                if (!sucesso) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao excluir anotação: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}