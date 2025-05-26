using Gradify.DTOs;
using Gradify.Services.Aulas;
using Microsoft.AspNetCore.Mvc;

namespace Gradify.Controllers
{
    public class AulaController : Controller
    {
        private readonly IAulaInterface _aulaService;

        public AulaController(IAulaInterface aulaService)
        {
            _aulaService = aulaService;
        }

        public async Task<IActionResult> Index()
        {
            var aulas = await _aulaService.GetAulas();
            return View(aulas);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(AulaDto aulaDto)
        {
            if (ModelState.IsValid)
            {
                await _aulaService.Criar(aulaDto);
                return RedirectToAction(nameof(Index));
            }
            return View(aulaDto);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var aula = await _aulaService.ObterPorId(id);
            if (aula == null) return NotFound();

            return View(aula);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, AulaDto aulaDto)
        {
            if (ModelState.IsValid)
            {
                await _aulaService.Editar(id, aulaDto);
                return RedirectToAction(nameof(Index));
            }
            return View(aulaDto);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            await _aulaService.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}