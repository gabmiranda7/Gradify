using Gradify.Dto;
using Gradify.Services.Professor;
using Gradify.Services.Turma;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gradify.Controllers
{
    public class TurmaController : Controller
    {
        private readonly ITurmaInterface _turmaService;
        private readonly IProfessorInterface _professorService;

        public TurmaController(ITurmaInterface turmaService, IProfessorInterface professorService)
        {
            _turmaService = turmaService;
            _professorService = professorService;
        }

        public IActionResult Index()
        {
            var turmas = _turmaService.GetTurmas();
            return View(turmas);
        }

        public IActionResult Criar()
        {
            var diasDaSemana = new List<string> { "Segunda", "Terça", "Quarta", "Quinta", "Sexta" };
            ViewBag.DiasDaSemana = diasDaSemana;

            var professores = _professorService.GetProfessores();
            ViewBag.Professores = professores;

            var dto = new TurmaCriacaoDto();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(TurmaCriacaoDto dto)
        {
            var diasValidos = new List<string> { "Segunda", "Terça", "Quarta", "Quinta", "Sexta" };

            if (!diasValidos.Contains(dto.DiaDaAula))
            {
                ModelState.AddModelError("DiaDaAula", "O dia da aula deve ser um dia válido da semana (Segunda, Terça, Quarta, Quinta, Sexta).");
                return View(dto);
            }

            if (!ModelState.IsValid)
                return View(dto);

            _turmaService.Criar(dto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id)
        {
            var turma = _turmaService.ObterPorId(id);
            if (turma == null)
                return NotFound();

            var professores = _professorService.GetProfessores();
            if (professores == null || !professores.Any())
            {
                ModelState.AddModelError(string.Empty, "Não há professores disponíveis.");
                return View();
            }

            ViewBag.Professores = new SelectList(professores, "Id", "Nome", turma.ProfessorId);

            var diasDaSemana = new List<string> { "Segunda", "Terça", "Quarta", "Quinta", "Sexta" };
            ViewBag.DiasDaSemana = diasDaSemana;

            var dto = new TurmaCriacaoDto
            {
                Materia = turma.Materia,
                DiaDaAula = turma.DiaDaAula,
                ProfessorId = turma.ProfessorId
            };

            ViewBag.TurmaId = id;
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, TurmaCriacaoDto dto)
        {
            var diasValidos = new List<string> { "Segunda", "Terça", "Quarta", "Quinta", "Sexta" };

            if (!diasValidos.Contains(dto.DiaDaAula))
            {
                ModelState.AddModelError("DiaDaAula", "O dia da aula deve ser um dia válido da semana (Segunda, Terça, Quarta, Quinta, Sexta).");
                return View(dto);
            }

            if (!ModelState.IsValid)
                return View(dto);

            var turma = _turmaService.Editar(id, dto);
            if (turma == null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int id)
        {
            var turma = _turmaService.ObterPorId(id);
            if (turma == null)
                return NotFound();

            return View(turma);
        }

        public IActionResult Excluir(int id)
        {
            var turma = _turmaService.ObterPorId(id);
            if (turma == null)
                return NotFound();

            return View(turma);
        }

        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarExclusao(int id)
        {
            var sucesso = _turmaService.Excluir(id);
            if (!sucesso)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
