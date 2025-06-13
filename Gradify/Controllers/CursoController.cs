using Gradify.DTOs;
using Gradify.Services.Cursos;
using Gradify.Services.Turmas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Gradify.Controllers
{
    public class CursoController : Controller
    {
        private readonly ICursoInterface _cursoService;
        private readonly ITurmaInterface _turmaService;

        public CursoController(ICursoInterface cursoService, ITurmaInterface turmaService)
        {
            _cursoService = cursoService;
            _turmaService = turmaService;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _cursoService.GetCursos();
            return View(cursos);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var curso = await _cursoService.ObterPorId(id);
            if (curso == null)
                return NotFound();

            return View(curso);
        }

        public async Task<IActionResult> Criar()
        {
            var professores = await _cursoService.GetProfessores();
            ViewBag.Professores = professores.Select(p => new SelectListItem
            {
                Value = p.Value.ToString(),
                Text = p.Text
            });

            return View(new CursoDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(CursoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var professores = await _cursoService.GetProfessores();
                ViewBag.Professores = professores.Select(p => new SelectListItem
                {
                    Value = p.Value.ToString(),
                    Text = p.Text
                });

                return View(dto);
            }

            await _cursoService.Criar(dto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Editar(int id)
        {
            var curso = await _cursoService.ObterPorId(id);
            if (curso == null)
                return NotFound();

            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(CursoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _cursoService.Editar(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            await _cursoService.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}