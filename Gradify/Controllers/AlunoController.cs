using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Gradify.Services.Alunos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gradify.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoInterface _alunoService;
        private readonly AppDbContext _context;

        public AlunoController(IAlunoInterface alunoService, AppDbContext context)
        {
            _alunoService = alunoService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var alunos = await _alunoService.GetAlunos();
            return View(alunos);
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null) return NotFound();

            var aluno = await _alunoService.ObterPorId(id.Value);
            if (aluno == null) return NotFound();

            return View(aluno);
        }

        public IActionResult Criar()
        {
            ViewData["Turmas"] = new SelectList(_context.Turmas, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(AlunoDTO alunoDto)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var usuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(usuarioId))
            {
                ModelState.AddModelError("", "Não foi possível identificar o usuário logado.");
                return View(alunoDto);
            }

            alunoDto.UsuarioId = usuarioId;
            {
                alunoDto.UsuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (ModelState.IsValid)
                {
                    await _alunoService.Criar(alunoDto);
                    return RedirectToAction(nameof(Index));
                }
                ViewData["Turmas"] = new SelectList(_context.Turmas, "Id", "Nome", alunoDto.TurmaId);
                return View(alunoDto);
            }
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var aluno = await _alunoService.ObterPorId(id.Value);
            if (aluno == null) return NotFound();

            ViewData["Turmas"] = new SelectList(_context.Turmas, "Id", "Nome", aluno.TurmaId);
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AlunoDTO alunoDto)
        {
            if (id != alunoDto.Id) return NotFound();

            alunoDto.UsuarioId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (ModelState.IsValid)
            {
                await _alunoService.Editar(alunoDto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Turmas"] = new SelectList(_context.Turmas, "Id", "Nome", alunoDto.TurmaId);
            return View(alunoDto);
        }
    }
}