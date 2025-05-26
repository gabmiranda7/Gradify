using Gradify.DTOs;
using Gradify.Models;
using Gradify.Services.Alunos;
using Gradify.Services.Frequencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public async Task<IActionResult> Index()
        {
            var alunos = await _alunoService.GetAlunos();
            return View(alunos);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var aluno = await _alunoService.ObterPorId(id);

            if (aluno == null)
            {
                return NotFound();
            }

            var frequencias =  _frequenciaService.BuscarFrequenciasPorAluno(id);

            aluno.Frequencias = frequencias;

            return View(aluno);
        }

        [Authorize(Roles = "Administrador, Professor")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Criar(AlunoDto alunoDto)
        {
            if (ModelState.IsValid)
            {
                var alunoCriado = await _alunoService.Criar(alunoDto);
                if (alunoCriado != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(alunoDto);
        }

        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Editar(int id)
        {
            var aluno = await _alunoService.ObterPorId(id);
            if (aluno == null)
            {
                return NotFound();
            }

            var alunoDto = new AlunoDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Matricula = aluno.Matricula
            };

            return View(alunoDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Editar(int id, AlunoDto alunoDto)
        {
            if (ModelState.IsValid)
            {
                var aluno = await _alunoService.Editar(id, alunoDto);
                if (aluno == null)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alunoDto);
        }

        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> Excluir(int id)
        {
            var aluno = await _alunoService.ObterPorId(id);
            if (aluno == null)
            {
                return NotFound();
            }

            var sucesso = await _alunoService.Excluir(id);
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
        [Authorize(Roles = "Administrador, Professor")]
        public async Task<IActionResult> ExcluirConfirmado(int id)
        {
            var sucesso = await _alunoService.Excluir(id);
            if (sucesso)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}