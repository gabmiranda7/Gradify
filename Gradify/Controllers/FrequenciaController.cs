using Gradify.Data;
using Gradify.DTOs;
using Gradify.Services.Frequencias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gradify.Controllers
{
    [Authorize(Roles = "Aluno")]
    public class FrequenciaController : Controller
    {
        private readonly IFrequenciaInterface _frequenciaService;
        private readonly AppDbContext _context;

        public FrequenciaController(IFrequenciaInterface frequenciaService, AppDbContext context)
        {
            _frequenciaService = frequenciaService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.UsuarioId == userId);

            var frequencias = await _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .Include(f => f.Aula)
                .Where(f => f.AlunoId == aluno.Id)
                .ToListAsync();

            ViewBag.ShowCreateButton = true;
            return View(frequencias);
        }

        public async Task<IActionResult> Criar()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await CarregarTurmas();
            ViewBag.Aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(FrequenciaDTO dto)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized();

            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);
            if (aluno == null)
                return NotFound("Aluno não encontrado.");

            dto.AlunoId = aluno.Id;

            if (!ModelState.IsValid)
            {
                await CarregarTurmas();
                ViewBag.Aluno = aluno;
                return View(dto);
            }

            await _frequenciaService.Criar(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var frequencia = await _frequenciaService.ObterPorId(id);
            if (frequencia == null)
                return NotFound();

            return View(frequencia);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var frequencia = await _frequenciaService.ObterPorId(id);
            if (frequencia == null)
                return NotFound();

            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized();

            var alunoLogado = await _context.Alunos
                .FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);

            if (alunoLogado == null)
                return NotFound("Aluno não encontrado.");

            if (frequencia.AlunoId != alunoLogado.Id)
                return Forbid();

            await CarregarTurmas();
            ViewBag.Aluno = alunoLogado;
            ViewBag.AlunoLogadoNome = alunoLogado.Nome;

            return View(frequencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(FrequenciaDTO dto)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized();

            var alunoLogado = await _context.Alunos
                .FirstOrDefaultAsync(a => a.UsuarioId == usuarioId);

            if (alunoLogado == null)
                return NotFound("Aluno não encontrado.");

            dto.AlunoId = alunoLogado.Id;

            if (!ModelState.IsValid)
            {
                await CarregarTurmas();
                ViewBag.Aluno = alunoLogado;
                ViewBag.AlunoLogadoNome = alunoLogado.Nome;
                return View(dto);
            }

            await _frequenciaService.Editar(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            await _frequenciaService.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarTurmas()
        {
            var turmas = await _context.Turmas
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Nome
                }).ToListAsync();

            ViewBag.Turmas = turmas;
        }
    }
}