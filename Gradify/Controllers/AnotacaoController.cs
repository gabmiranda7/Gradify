// Controller: AnotacaoController.cs
using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gradify.Controllers
{
    public class AnotacaoController : Controller
    {
        private readonly AppDbContext _context;

        public AnotacaoController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int aulaId)
        {
            var aula = await _context.Aulas.Include(a => a.Turma).FirstOrDefaultAsync(a => a.Id == aulaId);
            if (aula == null) return NotFound();

            var anotacao = await _context.Anotacoes.FirstOrDefaultAsync(a => a.AulaId == aulaId);

            var dto = new AnotacaoDTO
            {
                AulaId = aula.Id,
                Tema = aula.Tema,
                DataAula = aula.DataAula,
                Texto = anotacao?.Texto ?? string.Empty
            };
            ViewBag.Alunos = await _context.Alunos
                .Where(aluno => aluno.TurmaId == aula.TurmaId)
                .Select(aluno => new SelectListItem
                {
                    Value = aluno.Id.ToString(),
                    Text = aluno.Nome
                })
                .ToListAsync();

            var presencas = await _context.Frequencias
                .Include(f => f.Aluno)
                .Where(f => f.AulaId == aulaId)
                .ToListAsync();

            ViewBag.Presencas = presencas;



            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvarAnotacao(AnotacaoDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var aluno = await _context.Alunos
                .Include(a => a.Turma)
                .ThenInclude(t => t.Curso)
                .FirstOrDefaultAsync(a => a.UsuarioId == userId);

            if (aluno == null)
                return Unauthorized();

            var anotacao = await _context.Anotacoes
                .FirstOrDefaultAsync(a => a.AulaId == dto.AulaId && a.AlunoId == aluno.Id);

            if (anotacao == null)
            {
                anotacao = new Anotacao
                {
                    AulaId = dto.AulaId,
                    AlunoId = aluno.Id,
                    Texto = dto.Texto,
                    DataCriacao = DateTime.Now,
                    DataModificacao = DateTime.Now
                };
                _context.Anotacoes.Add(anotacao);
            }
            else
            {
                anotacao.Texto = dto.Texto;
                anotacao.DataModificacao = DateTime.Now;
                _context.Anotacoes.Update(anotacao);
            }

            await _context.SaveChangesAsync();


            TempData["Mensagem"] = "Anotação salva com sucesso!";
            return RedirectToAction("Index", new { aulaId = dto.AulaId });
        }


        [HttpPost]
        public async Task<IActionResult> RegistrarFrequencia(int aulaId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var aluno = await _context.Alunos
                .Include(a => a.Turma)
                .FirstOrDefaultAsync(a => a.UsuarioId == userId); // ajustado para UsuarioId, se for o campo correto

            if (aluno == null)
                return Unauthorized();

            var aula = await _context.Aulas
                .Include(a => a.Turma)
                .FirstOrDefaultAsync(a => a.Id == aulaId);

            if (aula == null)
                return NotFound();

            // Verifica se já existe registro
            bool jaRegistrado = await _context.Frequencias
                .AnyAsync(f => f.AulaId == aulaId && f.AlunoId == aluno.Id);

            if (!jaRegistrado)
            {
                var frequencia = new Frequencia
                {
                    AulaId = aula.Id,
                    AlunoId = aluno.Id,
                    Presente = true,
                    DataRegistro = DateTime.Now,
                    TurmaId = aula.TurmaId // 🔥 Aqui é o campo que você esqueceu de preencher antes
                };

                _context.Frequencias.Add(frequencia);
                await _context.SaveChangesAsync();
            }

            TempData["Mensagem"] = "Presença registrada com sucesso.";
            return RedirectToAction("Index", new { aulaId });
        }

    }
}
