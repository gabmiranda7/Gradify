using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
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

        public async Task<IEnumerable<CursoDto>> GetCursos()
        {
            return await _context.Cursos
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Descricao = c.Descricao,
                    ProfessorId = c.ProfessorId
                }).ToListAsync();
        }

        public async Task<CursoDto?> ObterPorId(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.TurmasCursos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null) return null;

            return new CursoDto
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                ProfessorId = curso.ProfessorId,
                TurmasCursos = curso.TurmasCursos
                    .Select(tc => new TurmaCursoDto
                    {
                        TurmaId = tc.TurmaId,
                        CursoId = tc.CursoId
                    }).ToList()
            };
        }

        public async Task<CursoDto?> Criar(CursoDto cursoDto)
        {
            var curso = new Curso
            {
                Nome = cursoDto.Nome,
                Descricao = cursoDto.Descricao,
                ProfessorId = cursoDto.ProfessorId
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            cursoDto.Id = curso.Id;
            return cursoDto;
        }

        public async Task<CursoDto?> Editar(int id, CursoDto cursoDto)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return null;

            curso.Nome = cursoDto.Nome;
            curso.Descricao = cursoDto.Descricao;
            curso.ProfessorId = cursoDto.ProfessorId;

            await _context.SaveChangesAsync();

            return cursoDto;
        }

        public async Task<bool> Excluir(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return false;

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}