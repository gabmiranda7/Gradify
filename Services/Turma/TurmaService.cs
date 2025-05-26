using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Services.Turma
{
    public class TurmaService : ITurmaInterface
    {
        private readonly AppDbContext _context;

        public TurmaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TurmaDto> Criar(TurmaDto dto)
        {
            var turma = new Models.Turma
            {
                Nome = dto.Nome,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim
            };

            _context.Turmas.Add(turma);
            _context.SaveChanges();

            return new TurmaDto
            {
                Id = turma.Id,
                Nome = turma.Nome,
                DataInicio = turma.DataInicio,
                DataFim = turma.DataFim
            };
        }

        public async Task<TurmaDto> Editar(int id, TurmaDto dto)
        {
            var turma = await _context.Turmas.FindAsync(id);
            if (turma == null) return null;

            turma.Nome = dto.Nome;
            turma.DataInicio = dto.DataInicio;
            turma.DataFim = dto.DataFim;

            _context.Turmas.Update(turma);
            await _context.SaveChangesAsync();

            return new TurmaDto
            {
                Id = turma.Id,
                Nome = turma.Nome,
                DataInicio = turma.DataInicio,
                DataFim = turma.DataFim
            };
        }

        public async Task<IEnumerable<TurmaDto>> GetTurmas()
        {
            return await _context.Turmas
                .Select(t => new TurmaDto
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    DataInicio = t.DataInicio,
                    DataFim = t.DataFim
                })
                .ToListAsync();
        }

        public async Task<TurmaDto> ObterPorId(int id)
        {
            var turma = await _context.Turmas
                .FirstOrDefaultAsync(t => t.Id == id);

            if (turma == null) return null;

            return new TurmaDto
            {
                Id = turma.Id,
                Nome = turma.Nome,
                DataInicio = turma.DataInicio,
                DataFim = turma.DataFim
            };
        }

        public async Task<bool> Excluir(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            if (turma == null) return false;

            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}