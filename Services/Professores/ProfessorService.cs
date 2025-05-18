using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Services.Professores
{
    public class ProfessorService : IProfessorInterface
    {
        private readonly AppDbContext _context;

        public ProfessorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProfessorDto?> ObterPorId(int id)
        {
            try
            {
                var professor = await _context.Professores.FindAsync(id);
                if (professor == null) return null;

                return new ProfessorDto
                {
                    Id = professor.Id,
                    Nome = professor.Nome,
                    Email = professor.Email
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProfessorDto>> GetProfessores()
        {
            try
            {
                return await _context.Professores
                    .Select(p => new ProfessorDto
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Email = p.Email
                    })
                    .ToListAsync();
            }
            catch
            {
                return new List<ProfessorDto>();
            }
        }

        public async Task<ProfessorDto> Criar(ProfessorDto professorDto)
        {
            try
            {
                var professor = new Professor
                {
                    Nome = professorDto.Nome,
                    Email = professorDto.Email
                };

                _context.Professores.Add(professor);
                await _context.SaveChangesAsync();

                professorDto.Id = professor.Id;
                return professorDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProfessorDto?> Editar(int id, ProfessorDto professorDto)
        {
            try
            {
                var professor = await _context.Professores.FindAsync(id);
                if (professor == null) return null;

                professor.Nome = professorDto.Nome;
                professor.Email = professorDto.Email;

                await _context.SaveChangesAsync();
                return professorDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                var professor = await _context.Professores.FindAsync(id);
                if (professor == null) return false;

                _context.Professores.Remove(professor);
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