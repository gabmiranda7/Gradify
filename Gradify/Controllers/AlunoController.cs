using Gradify.Dto;
using Gradify.Models;
using Gradify.Services.Aluno;
using Gradify.Services.Frequencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Gradify.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoInterface _alunoService;
        private readonly IFrequenciaInterface _frequenciaService;

        public AlunoController(IAlunoInterface alunoService, IFrequenciaInterface frequenciaService)
        {
            _alunoService = alunoService;
            _frequenciaService = frequenciaService;
        }

        [Authorize(Roles = "Aluno, Professor")]

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

            var frequencias = _frequenciaService.BuscarFrequenciasPorAluno(id);

            aluno.Frequencias = frequencias;

            return View(aluno);
        }

        [Authorize(Roles = "Professor")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Professor")]
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

        [Authorize(Roles = "Professor")]
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
        [Authorize(Roles = "Professor")]
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

        [Authorize(Roles = "Professor")]
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
        [Authorize(Roles = "Professor")]
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