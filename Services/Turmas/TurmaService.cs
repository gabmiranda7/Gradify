using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Services.Turmas
{
    public class TurmaService : ITurmaInterface
    {
        private readonly AppDbContext _context;

        public TurmaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TurmaDto>> GetTurmas()
        {
            try
            {
                return await _context.Turmas
                    .Include(t => t.AlunosTurmas)
                    .Include(t => t.Anotacoes)
                    .Include(t => t.Aulas)
                    .Include(t => t.Frequencias)
                    .Include(t => t.TurmasCursos)
                    .Select(t => new TurmaDto
                    {
                        Id = t.Id,
                        DataInicio = t.DataInicio,
                        DataFim = t.DataFim,
                        AlunosTurmas = t.AlunosTurmas.Select(at => new AlunoTurmaDto { AlunoId = at.AlunoId, TurmaId = at.TurmaId }).ToList(),
                        Anotacoes = t.Anotacoes.Select(a => new AnotacaoDto { Id = a.Id, Texto = a.Texto }).ToList(),
                        Aulas = t.Aulas.Select(a => new AulaDto { Id = a.Id, DataAula = a.DataAula }).ToList(),
                        TurmasCursos = t.TurmasCursos.Select(tc => new TurmaCursoDto { TurmaId = tc.TurmaId, CursoId = tc.CursoId }).ToList()
                    })
                    .ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<TurmaDto>();
            }
        }

        public async Task<TurmaDto?> ObterPorId(int id)
        {
            try
            {
                var t = await _context.Turmas
                    .Include(t => t.AlunosTurmas)
                    .Include(t => t.Anotacoes)
                    .Include(t => t.Aulas)
                    .Include(t => t.Frequencias)
                    .Include(t => t.TurmasCursos)
                    .FirstOrDefaultAsync(t => t.Id == id);

                if (t == null) return null;

                return new TurmaDto
                {
                    Id = t.Id,
                    DataInicio = t.DataInicio,
                    DataFim = t.DataFim,
                    AlunosTurmas = t.AlunosTurmas.Select(at => new AlunoTurmaDto { AlunoId = at.AlunoId, TurmaId = at.TurmaId }).ToList(),
                    Anotacoes = t.Anotacoes.Select(a => new AnotacaoDto { Id = a.Id, Texto = a.Texto }).ToList(),
                    Aulas = t.Aulas.Select(a => new AulaDto { Id = a.Id, DataAula = a.DataAula }).ToList(),
                    TurmasCursos = t.TurmasCursos.Select(tc => new TurmaCursoDto { TurmaId = tc.TurmaId, CursoId = tc.CursoId }).ToList()
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<TurmaDto> Criar(TurmaDto turmaDto)
        {
            try
            {
                var turma = new Turma
                {
                    DataInicio = turmaDto.DataInicio,
                    DataFim = turmaDto.DataFim
                };

                _context.Turmas.Add(turma);
                await _context.SaveChangesAsync();

                turmaDto.Id = turma.Id;
                return turmaDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TurmaDto?> Editar(int id, TurmaDto turmaDto)
        {
            try
            {
                var turma = await _context.Turmas.FindAsync(id);
                if (turma == null) return null;

                turma.DataInicio = turmaDto.DataInicio;
                turma.DataFim = turmaDto.DataFim;

                await _context.SaveChangesAsync();
                return turmaDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                var turma = await _context.Turmas.FindAsync(id);
                if (turma == null) return false;

                _context.Turmas.Remove(turma);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}