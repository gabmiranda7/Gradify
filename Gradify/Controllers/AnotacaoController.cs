using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Gradify.Services.Anotacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Controllers
{
    [Authorize]
    public class AnotacaoController : Controller
    {
        private readonly IAnotacaoInterface _anotacaoService;
        private readonly UserManager<Usuario> _userManager;
        private readonly AppDbContext _context;

        public AnotacaoController(IAnotacaoInterface anotacaoService, UserManager<Usuario> userManager, AppDbContext context)
        {
            _anotacaoService = anotacaoService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioLogado = await _userManager.GetUserAsync(User);

            if (usuarioLogado == null)
            {
                TempData["Erro"] = "Usuário não autenticado.";
                return RedirectToAction("Index", "Home");
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(a => a.UsuarioId == usuarioLogado.Id);

            if (aluno == null)
            {
                TempData["Erro"] = $"Aluno não encontrado para o usuário logado (ID: {usuarioLogado.Id})";
                return RedirectToAction("Index", "Home");
            }

            var anotacoes = await _anotacaoService.GetAnotacoesPorAlunoId(aluno.Id);
            return View(anotacoes);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var anotacao = await _anotacaoService.ObterPorId(id);
            if (anotacao == null) return NotFound();
            return View(anotacao);
        }

        public async Task<IActionResult> Criar()
        {
            var usuarioLogado = await _userManager.GetUserAsync(User);
            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.UsuarioId == usuarioLogado.Id);

            if (aluno == null)
            {
                TempData["Erro"] = "Aluno não encontrado para o usuário logado.";
                return RedirectToAction("Index");
            }

            var dto = new AnotacaoDTO
            {
                AlunoId = aluno.Id,
                AlunoNome = aluno.Nome
            };

            ViewBag.Cursos = await _anotacaoService.GetCursosSelectList();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(AnotacaoDTO dto)
        {
            var usuarioLogado = await _userManager.GetUserAsync(User);
            var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.UsuarioId == usuarioLogado.Id);

            if (aluno == null)
            {
                TempData["Erro"] = "Aluno não encontrado para o usuário logado.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                dto.AlunoId = aluno.Id;
                dto.DataCriacao = DateTime.Now;
                dto.DataModificacao = DateTime.Now;
                await _anotacaoService.Criar(dto);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cursos = await _anotacaoService.GetCursosSelectList();
            return View(dto);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var anotacao = await _anotacaoService.ObterPorId(id);
            if (anotacao == null) return NotFound();

            ViewBag.Cursos = await _anotacaoService.GetCursosSelectList();
            return View(anotacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AnotacaoDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                dto.DataModificacao = DateTime.Now;
                await _anotacaoService.Editar(dto);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cursos = await _anotacaoService.GetCursosSelectList();
            return View(dto);
        }
    }
}