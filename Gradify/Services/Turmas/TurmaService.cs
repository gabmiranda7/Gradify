using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Gradify.Services.Turmas;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gradify.Services.Turmas
{
    public class TurmaService : ITurmaInterface
    {
        private readonly AppDbContext _context;

        public TurmaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TurmaDTO>> GetTurmas()
        {
            return await _context.Turmas
                //.Include(t => t.Professor)
                .Select(t => new TurmaDTO
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    DataInicio = t.DataInicio,
                    DataFim = t.DataFim,
                    //ProfessorId = t.ProfessorId,
                    //NomeProfessor = t.Professor.Nome
                })
                .ToListAsync();
        }

        public async Task<TurmaDTO?> ObterPorId(int id)
        {
            var turma = await _context.Turmas
                //.Include(t => t.Professor)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (turma == null) return null;

            return new TurmaDTO
            {
                Id = turma.Id,
                Nome = turma.Nome,
                DataInicio = turma.DataInicio,
                DataFim = turma.DataFim,
                //ProfessorId = turma.ProfessorId,
                //NomeProfessor = turma.Professor.Nome
            };
        }

        public async Task Criar(TurmaDTO dto)
        {
            var turma = new Turma
            {
                Nome = dto.Nome,
                DataInicio = dto.DataInicio,
                DataFim = dto.DataFim,
                //ProfessorId = dto.ProfessorId
            };

            _context.Turmas.Add(turma);
            await _context.SaveChangesAsync();
        }

        public async Task Editar(TurmaDTO dto)
        {
            var turma = await _context.Turmas.FindAsync(dto.Id);
            if (turma == null) return;

            turma.Nome = dto.Nome;
            turma.DataInicio = dto.DataInicio;
            turma.DataFim = dto.DataFim;
            //turma.ProfessorId = dto.ProfessorId;

            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            if (turma != null)
            {
                _context.Turmas.Remove(turma);
                await _context.SaveChangesAsync();
            }
        }
    }
}