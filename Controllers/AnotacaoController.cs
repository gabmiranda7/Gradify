using Gradify.DTOs;
using Gradify.Models;
using Gradify.Services.Anotacao;
using Gradify.Services.Turma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Gradify.Controllers
{
    public class AnotacaoController : Controller
    {
        private readonly IAnotacaoInterface _service;
        private readonly ITurmaInterface _turmaService;
        private readonly UserManager<Usuario> _userManager;

        public AnotacaoController(IAnotacaoInterface service, ITurmaInterface turmaService, UserManager<Usuario> userManager)
        {
            _service = service;
            _turmaService = turmaService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public IActionResult Index()
        {
            var anotacoes = _service.GetAnotacoes();
            return View(anotacoes);
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public IActionResult Detalhes(int id)
        {
            var anotacao = _service.ObterPorId(id);
            if (anotacao == null) return NotFound();
            return View(anotacao);
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public IActionResult Criar()
        {
            CarregarTurmas();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public async Task<IActionResult> Criar(AnotacaoDto dto)
        {
            if (!ModelState.IsValid)
            {
                CarregarTurmas();
                return View(dto);
            }

            var usuario = await _userManager.GetUserAsync(User);  
            if (usuario == null)
            {
                TempData["Erro"] = "Usuário não logado.";
                return RedirectToAction("Login", "Conta"); 
            }

            _service.Criar(dto); 
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public IActionResult Editar(int id)
        {
            var anotacao = _service.ObterPorId(id);
            if (anotacao == null) return NotFound();

            var dto = new AnotacaoDto
            {
                Texto = anotacao.Texto,
                TurmaId = anotacao.TurmaId
            };

            CarregarTurmas();
            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public IActionResult Editar(int id, AnotacaoDto dto)
        {
            if (!ModelState.IsValid)
            {
                CarregarTurmas();
                return View(dto);
            }

            _service.Editar(id, dto);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public IActionResult Excluir(int id)
        {
            var anotacao = _service.ObterPorId(id);
            if (anotacao == null)
            {
                return NotFound();
            }

            var sucesso = _service.Excluir(id);
            if (sucesso)
            {
                TempData["Sucesso"] = "Anotação excluída com sucesso!";
            }
            else
            {
                TempData["Erro"] = "Erro ao excluir a anotação.";
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        private async Task CarregarTurmas()
        {
            var turmas = await _turmaService.GetTurmas();
            ViewBag.Turmas = new SelectList(turmas, "Id", "Materia");
        }
    }
}