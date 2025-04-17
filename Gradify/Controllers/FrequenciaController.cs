using Gradify.Dto;
using Gradify.Services.Aluno;
using Gradify.Services.Frequencia;
using Gradify.Services.Turma;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Index()
        {
            var frequencias = _frequenciaService.GetFrequencias();
            var alunos = _alunoService.GetAlunos().ToDictionary(a => a.Id, a => a.Nome);
            var turmas = _turmaService.GetTurmas().ToDictionary(t => t.Id, t => t.Materia);
            var frequenciasDto = frequencias.Select(f => new FrequenciaLeituraDto
            {
                Id = f.Id,
                AlunoId = f.AlunoId,
                TurmaId = f.TurmaId,
                Data = f.Data,
                AlunoNome = alunos.ContainsKey(f.AlunoId) ? alunos[f.AlunoId] : "Desconhecido",
                Materia = turmas.ContainsKey(f.TurmaId) ? turmas[f.TurmaId] : "Desconhecida"
            }).ToList();

            return View(frequenciasDto);
        }

        public IActionResult Criar()
        {
            var alunos = _alunoService.GetAlunos();
            var turmas = _turmaService.GetTurmas();
            ViewBag.Alunos = new SelectList(alunos, "Id", "Nome");
            ViewBag.Turmas = new SelectList(turmas, "Id", "Materia");
            return View();
        }

        [HttpPost]
        public IActionResult Criar(FrequenciaCriacaoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = _frequenciaService.Criar(dto);
            if (result == null)
            {
                ModelState.AddModelError("", "Não é possível registrar a frequência fora do dia da aula.");
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Detalhes(int id)
        {
            var frequencia = _frequenciaService.ObterPorId(id);
            if (frequencia == null) return NotFound();

            return View(frequencia);
        }

        public IActionResult Editar(int id)
        {
            var frequencia = _frequenciaService.ObterPorId(id);
            if (frequencia == null) return NotFound();

            var alunos = _alunoService.GetAlunos();
            var turmas = _turmaService.GetTurmas();
            ViewBag.Alunos = new SelectList(alunos, "Id", "Nome", frequencia.AlunoId);
            ViewBag.Turmas = new SelectList(turmas, "Id", "Materia", frequencia.TurmaId);

            return View(new FrequenciaCriacaoDto
            {
                AlunoId = frequencia.AlunoId,
                TurmaId = frequencia.TurmaId,
                Data = frequencia.Data
            });
        }

        [HttpPost]
        public IActionResult Editar(int id, FrequenciaCriacaoDto dto)
        {
            var resultado = _frequenciaService.Editar(id, dto);
            if (resultado == null)
            {
                ModelState.AddModelError("", "Erro ao editar a frequência.");
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Excluir(int id)
        {
            var frequencia = _frequenciaService.ObterPorId(id);
            if (frequencia == null) return NotFound();

            var sucesso = _frequenciaService.Excluir(id);
            if (!sucesso) return NotFound();

            return RedirectToAction("Index");
        }
    }
}