﻿using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Gradify.Controllers
{
    public class AulaController : Controller
    {
        private readonly AppDbContext _context;

        public AulaController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int turmaId)
        {
            var aulas = await _context.Aulas
                .Include(a => a.Professor)
                .Where(a => a.TurmaId == turmaId)
                .OrderBy(a => a.DataAula)
                .ToListAsync();

            var dtoList = aulas.Select(a => new AulaDTO
            {
                Id = a.Id,
                Tema = a.Tema,
                DataAula = a.DataAula,
                TurmaId = a.TurmaId,
                NomeProfessor = a.Professor?.Nome
            }).ToList();

         var turma = await _context.Turmas
                .Include(t => t.Curso)
                .FirstOrDefaultAsync(t => t.Id == turmaId);

            ViewBag.TurmaId = turmaId;
            ViewBag.CursoId = turma?.CursoId;

            return View(dtoList);
        }


        // GET: Aula/Criar
        public async Task<IActionResult> Criar(int turmaId)
        {
            var dto = new AulaDTO
            {
                TurmaId = turmaId,
                DataAula = DateTime.Today
            };

            ViewBag.Professores = new SelectList(
                await _context.Professores.ToListAsync(),
                "Id",
                "Nome"
            );

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(AulaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Erro: " + modelError.ErrorMessage);
                }

                ViewBag.Professores = new SelectList(
                    await _context.Professores.ToListAsync(),
                    "Id",
                    "Nome"
                );

                return View(dto);
            }

            var aula = new Aula
            {
                Tema = dto.Tema,
                DataAula = dto.DataAula,
                TurmaId = dto.TurmaId,
                ProfessorId = dto.ProfessorId // se estiver usando professor
            };

            _context.Aulas.Add(aula);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { turmaId = dto.TurmaId });
        }

        // GET: Aula/Editar
        public async Task<IActionResult> Editar(int id)
        {
            var aula = await _context.Aulas.FindAsync(id);
            if (aula == null) return NotFound();

            var dto = new AulaDTO
            {
                Id = aula.Id,
                Tema = aula.Tema,
                DataAula = aula.DataAula,
                TurmaId = aula.TurmaId,
                //ProfessorId = aula.ProfessorId // se estiver usando professor
            };

            ViewBag.Professores = new SelectList(
                await _context.Professores.ToListAsync(),
                "Id",
                "Nome",
                aula.ProfessorId
            );



            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(AulaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Erro: " + modelError.ErrorMessage);
                }

                ViewBag.Professores = new SelectList(
                    await _context.Professores.ToListAsync(),
                    "Id",
                    "Nome",
                    dto.ProfessorId
                );

                return View(dto);
            }

            var aula = await _context.Aulas.FindAsync(dto.Id);
            if (aula == null) return NotFound();

            aula.Tema = dto.Tema;
            aula.DataAula = dto.DataAula;
            aula.ProfessorId = dto.ProfessorId; // se estiver usando professor

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { turmaId = dto.TurmaId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(int id)
        {
            var aula = await _context.Aulas.FindAsync(id);
            if (aula == null)
                return NotFound();

            _context.Aulas.Remove(aula);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { turmaId = aula.TurmaId });
        }
    }
}