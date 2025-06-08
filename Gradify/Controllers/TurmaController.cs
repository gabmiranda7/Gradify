using Gradify.Data;
using Gradify.DTOs;
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

        public async Task<IActionResult> Index()
        {
            var turmas = await _turmaService.GetTurmas();
            return View(turmas);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var turma = await _turmaService.ObterPorId(id);
            if (turma == null)
                return NotFound();

            return View(turma);
        }

        public IActionResult Criar()
        {
            CarregarProfessores();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(TurmaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                CarregarProfessores();
                return View(dto);
            }

            await _turmaService.Criar(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var turma = await _turmaService.ObterPorId(id);
            if (turma == null)
                return NotFound();

            CarregarProfessores();
            return View(turma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(TurmaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                CarregarProfessores();
                return View(dto);
            }

            await _turmaService.Editar(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            await _turmaService.Excluir(id);
            return RedirectToAction(nameof(Index));
        }

        private void CarregarProfessores()
        {
            var professores = _context.Professores
                .OrderBy(p => p.Nome)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nome
                })
                .ToList();

            ViewBag.Professores = professores;
        }
    }
}