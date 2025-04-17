using Gradify.Dto;
using Gradify.Models;
using Gradify.Services.Aluno;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Gradify.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoInterface _alunoService;

        public AlunoController(IAlunoInterface alunoService)
        {
            _alunoService = alunoService;
        }

        public IActionResult Index()
        {
            var alunos = _alunoService.GetAlunos();
            return View(alunos);
        }

        public IActionResult Detalhes(int id)
        {
            var aluno = _alunoService.ObterPorId(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(AlunoCriacaoDto alunoDto)
        {
            if (ModelState.IsValid)
            {
                var alunoCriado = _alunoService.Criar(alunoDto);
                if (alunoCriado != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(alunoDto);
        }

        public IActionResult Editar(int id)
        {
            var aluno = _alunoService.ObterPorId(id);
            if (aluno == null)
            {
                return NotFound();
            }

            var alunoDto = new AlunoCriacaoDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Matricula = aluno.Matricula
            };

            return View(alunoDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, AlunoCriacaoDto alunoDto)
        {
            if (ModelState.IsValid)
            {
                var aluno = _alunoService.Editar(id, alunoDto);
                if (aluno == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alunoDto);
        }

        public IActionResult Excluir(int id)
        {
            var aluno = _alunoService.ObterPorId(id);
            if (aluno == null)
            {
                return NotFound();
            }

            var sucesso = _alunoService.Excluir(id);
            if (sucesso)
            {
                TempData["Sucesso"] = "Aluno excluído com sucesso!";
            }
            else
            {
                TempData["Erro"] = "Erro ao excluir o aluno.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ExcluirConfirmado(int id)
        {
            var sucesso = _alunoService.Excluir(id);
            if (sucesso)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
