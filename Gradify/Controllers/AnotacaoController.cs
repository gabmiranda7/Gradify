using Gradify.Dto;
using Gradify.Services.Anotacao;
using Gradify.Services.Turma;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gradify.Controllers
{
    public class AnotacaoController : Controller
    {
        private readonly IAnotacaoInterface _service;
        private readonly ITurmaInterface _turmaService;

        public AnotacaoController(IAnotacaoInterface service, ITurmaInterface turmaService)
        {
            _service = service;
            _turmaService = turmaService;
        }

        public IActionResult Index()
        {
            var anotacoes = _service.GetAnotacoes();
            return View(anotacoes);
        }

        public IActionResult Detalhes(int id)
        {
            var anotacao = _service.ObterPorId(id);
            if (anotacao == null) return NotFound();
            return View(anotacao);
        }

        public IActionResult Criar()
        {
            CarregarTurmas();
            return View();
        }

        [HttpPost]
        public IActionResult Criar(AnotacaoCriacaoDto dto)
        {
            if (!ModelState.IsValid)
            {
                CarregarTurmas();
                return View(dto);
            }

            _service.Criar(dto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id)
        {
            var anotacao = _service.ObterPorId(id);
            if (anotacao == null) return NotFound();

            var dto = new AnotacaoCriacaoDto
            {
                Comentario = anotacao.Comentario,
                TurmaId = anotacao.TurmaId
            };

            CarregarTurmas();
            return View(dto);
        }

        [HttpPost]
        public IActionResult Editar(int id, AnotacaoCriacaoDto dto)
        {
            if (!ModelState.IsValid)
            {
                CarregarTurmas();
                return View(dto);
            }

            _service.Editar(id, dto);
            return RedirectToAction(nameof(Index));
        }

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

        private void CarregarTurmas()
        {
            var turmas = _turmaService.GetTurmas();
            ViewBag.Turmas = new SelectList(turmas, "Id", "Materia");
        }
    }
}
