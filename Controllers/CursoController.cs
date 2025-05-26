using Gradify.DTOs;
using Gradify.Services.Cursos;
using Microsoft.AspNetCore.Mvc;

namespace Gradify.Controllers
{
    public class CursoController : Controller
    {
        private readonly ICursoInterface _cursoService;

        public CursoController(ICursoInterface cursoService)
        {
            _cursoService = cursoService;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _cursoService.GetCursos();
            return View(cursos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CursoDto cursoDto)
        {
            if (ModelState.IsValid)
            {
                await _cursoService.Criar(cursoDto);
                return RedirectToAction(nameof(Index));
            }
            return View(cursoDto);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var curso = await _cursoService.ObterPorId(id);
            if (curso == null) return NotFound();

            return View(curso);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, CursoDto cursoDto)
        {
            if (ModelState.IsValid)
            {
                await _cursoService.Editar(id, cursoDto);
                return RedirectToAction(nameof(Index));
            }
            return View(cursoDto);
        }

        public async Task<IActionResult> Excluir(int id)
        {
            await _cursoService.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}