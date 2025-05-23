using Gradify.DTOs;
using Gradify.Services.Frequencias;
using Microsoft.AspNetCore.Mvc;

namespace Gradify.Controllers
{
    public class FrequenciaController : Controller
    {
        private readonly IFrequenciaInterface _frequenciaService;

        public FrequenciaController(IFrequenciaInterface frequenciaService)
        {
            _frequenciaService = frequenciaService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _frequenciaService.GetFrequencias();
            return View(lista);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(FrequenciaDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            try
            {
                await _frequenciaService.Criar(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao criar frequência: {ex.Message}");
                return View(dto);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var dto = await _frequenciaService.ObterPorId(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, FrequenciaDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            try
            {
                var atualizado = await _frequenciaService.Editar(id, dto);
                if (atualizado == null) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao editar frequência: {ex.Message}");
                return View(dto);
            }
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var dto = await _frequenciaService.ObterPorId(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var sucesso = await _frequenciaService.Excluir(id);
                if (!sucesso) return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao excluir frequência: {ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }
    }
}