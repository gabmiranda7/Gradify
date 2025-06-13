using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Gradify.Services.Turmas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Controllers
{
    public class TurmaController : Controller
    {
        private readonly ITurmaInterface _turmaService;
        private readonly AppDbContext _context;

        public TurmaController(ITurmaInterface turmaService, AppDbContext context)
        {
            _turmaService = turmaService;
            _context = context;
        }

        public async Task<IActionResult> Index(int cursoId)
        {
            var turmas = await _context.Turmas
                .Where(t => t.CursoId == cursoId)
                //.Include(t => t.Professor)
                .ToListAsync();

            var turmasDTO = turmas.Select(t => new TurmaDTO
            {
                Id = t.Id,
                Nome = t.Nome,
                //NomeProfessor = t.Professor?.Nome ?? ""
            });

            ViewBag.CursoId = cursoId;
            return View(turmasDTO);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var turma = await _turmaService.ObterPorId(id);
            if (turma == null)
                return NotFound();

            return View(turma);
        }

        public async Task<IActionResult> Criar(int cursoId)
        {
            var dto = new TurmaDTO
            {
                CursoId = cursoId
            };

            await CarregarProfessores();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(TurmaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarProfessores();
                return View(dto);
            }

            var turma = new Turma
            {
                Nome = dto.Nome,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                //ProfessorId = dto.ProfessorId,
                CursoId = dto.CursoId
            };

            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { cursoId = dto.CursoId });
        }

        public async Task<IActionResult> Editar(int id)
        {
            var turma = await _turmaService.ObterPorId(id);
            if (turma == null)
                return NotFound();

            await CarregarProfessores();
            return View(turma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(TurmaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarProfessores();
                return View(dto);
            }

            await _turmaService.Editar(dto);
            return RedirectToAction("Index", new { cursoId = dto.CursoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id, int cursoId)
        {
            await _turmaService.Excluir(id);
            return RedirectToAction("Index", new { cursoId });
        }

        public async Task CarregarProfessores()
        {
            var professores = await _context.Professores
                .OrderBy(p => p.Nome)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nome
                })
                .ToListAsync();

            ViewBag.Professores = professores;
        }
    }
}