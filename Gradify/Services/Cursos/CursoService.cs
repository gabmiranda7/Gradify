using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Gradify.Services.Cursos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Services.Cursos
{
    public class CursoService : ICursoInterface
    {
        private readonly AppDbContext _context;

        public CursoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CursoDTO>> GetCursos()
        {
            return await _context.Cursos
                //.Include(c => c.Professor)
                .Include(c => c.Turmas)
                .Select(c => new CursoDTO
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Descricao = c.Descricao,
                    //ProfessorId = c.ProfessorId,
                    //ProfessorNome = c.Professor.Nome,
                    TurmaIds = c.Turmas.Select(tc => tc.Id).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<SelectListItem>> GetProfessores()
        {
            return await _context.Professores
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nome
                })
                .ToListAsync();
        }

        public async Task<CursoDTO?> ObterPorId(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Turmas)
                //.Include(c => c.Professor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null) return null;

            return new CursoDTO
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                //ProfessorId = curso.ProfessorId,
                //ProfessorNome = curso.Professor?.Nome,
            };
        }

        public async Task Criar(CursoDTO dto)
        {
            var curso = new Curso
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                //ProfessorId = dto.ProfessorId,
            };

            foreach (var turmaId in dto.TurmaIds)
            {
                curso.Turmas.Add(new Turma { Id = turmaId });
            }

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();
        }

        public async Task Editar(CursoDTO dto)
        {
            var curso = await _context.Cursos
                .Include(c => c.Turmas)
                .FirstOrDefaultAsync(c => c.Id == dto.Id);

            if (curso == null) return;

            curso.Nome = dto.Nome;
            curso.Descricao = dto.Descricao;
            //curso.ProfessorId = dto.ProfessorId;

            var turmasExistentes = curso.Turmas.Select(tc => tc.Id).ToList();

            foreach (var turmaCurso in curso.Turmas.ToList())
            {
                if (!dto.TurmaIds.Contains(turmaCurso.Id))
                {
                    curso.Turmas.Remove(turmaCurso);
                }
            }

            foreach (var turmaId in dto.TurmaIds)
            {
                if (!turmasExistentes.Contains(turmaId))
                {
                    curso.Turmas.Add(new Turma { Id = turmaId });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
            }
        }
    }
}
