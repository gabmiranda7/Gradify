using Gradify.DTOs;
using Gradify.Services.Alunos;
using Gradify.Services.Frequencia;
using Gradify.Services.Turma;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Gradify.Controllers
{
    public class FrequenciaController : Controller
    {
        private readonly IFrequenciaInterface _frequenciaService;
        private readonly IAlunoInterface _alunoService;
        private readonly ITurmaInterface _turmaService;

        public FrequenciaController(
            IFrequenciaInterface frequenciaService,
            IAlunoInterface alunoService,
            ITurmaInterface turmaService)
        {
            _frequenciaService = frequenciaService;
            _alunoService = alunoService;
            _turmaService = turmaService;
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public async Task<IActionResult> Index()
        {
            var frequencias = _frequenciaService.GetFrequencias();

            var alunos = await _alunoService.GetAlunos();
            var turmas = await _turmaService.GetTurmas();

            var alunosDict = alunos.ToDictionary(a => a.Id, a => a.Nome);
            var turmasDict = turmas.ToDictionary(t => t.Id, t => t.Nome);

            var frequenciasDto = frequencias.Select(f => new FrequenciaDto
            {
                Id = f.Id,
                AlunoId = f.AlunoId,
                TurmaId = f.TurmaId,
                Data = f.Data,
                AlunoNome = alunosDict.ContainsKey(f.AlunoId) ? alunosDict[f.AlunoId] : "Desconhecido",
                TurmaNome = turmasDict.ContainsKey(f.TurmaId) ? turmasDict[f.TurmaId] : "Desconhecida"
            }).ToList();

            return View(frequenciasDto);
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public async Task<IActionResult> Criar()
        {
            var alunos = await _alunoService.GetAlunos();
            var turmas = await _turmaService.GetTurmas();

            ViewBag.Alunos = new SelectList(alunos, "Id", "Nome");
            ViewBag.Turmas = new SelectList(turmas, "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public async Task<IActionResult> Criar(FrequenciaDto dto)
        {
            if (!ModelState.IsValid)
            {
                var alunos = await _alunoService.GetAlunos();
                var turmas = await _turmaService.GetTurmas();

                ViewBag.Alunos = new SelectList(alunos, "Id", "Nome", dto.AlunoId);
                ViewBag.Turmas = new SelectList(turmas, "Id", "Nome", dto.TurmaId);

                return View(dto);
            }

            var result = _frequenciaService.Criar(dto);
            if (result == null)
            {
                ModelState.AddModelError("", "Erro ao registrar a frequência.");

                var alunos = await _alunoService.GetAlunos();
                var turmas = await _turmaService.GetTurmas();

                ViewBag.Alunos = new SelectList(alunos, "Id", "Nome", dto.AlunoId);
                ViewBag.Turmas = new SelectList(turmas, "Id", "Nome", dto.TurmaId);

                return View(dto);
            }

            TempData["Sucesso"] = "Frequência registrada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public IActionResult Detalhes(int id)
        {
            var frequencia = _frequenciaService.ObterPorId(id);
            if (frequencia == null)
                return NotFound();

            return View(frequencia);
        }

        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public async Task<IActionResult> Editar(int id)
        {
            var frequencia = _frequenciaService.ObterPorId(id);
            if (frequencia == null)
                return NotFound();

            var alunos = await _alunoService.GetAlunos();
            var turmas = await _turmaService.GetTurmas();

            ViewBag.Alunos = new SelectList(alunos, "Id", "Nome", frequencia.AlunoId);
            ViewBag.Turmas = new SelectList(turmas, "Id", "Nome", frequencia.TurmaId);

            var dto = new FrequenciaDto
            {
                Id = frequencia.Id,
                AlunoId = frequencia.AlunoId,
                TurmaId = frequencia.TurmaId,
                Data = frequencia.Data,
                AlunoNome = frequencia.AlunoNome,
                TurmaNome = frequencia.TurmaNome
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public IActionResult Editar(int id, FrequenciaDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var resultado = _frequenciaService.Editar(id, dto);
            if (resultado == null)
            {
                ModelState.AddModelError("", "Erro ao editar a frequência.");
                return View(dto);
            }

            TempData["Sucesso"] = "Frequência editada com sucesso.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Aluno, Administrador, Professor")]
        public IActionResult ExcluirConfirmado(int id)
        {
            var sucesso = _frequenciaService.Excluir(id);
            if (!sucesso)
            {
                TempData["Erro"] = "Erro ao excluir a frequência.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Sucesso"] = "Frequência excluída com sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}