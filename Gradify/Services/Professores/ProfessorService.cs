using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gradify.Services.Professores
{
    public class ProfessorService : IProfessorInterface
    {
        private readonly AppDbContext _context;

        public ProfessorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProfessorDTO>> GetProfessores()
        {
            return await _context.Professores
                .Select(p => new ProfessorDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Email = p.Email
                })
                .ToListAsync();
        }

        public async Task<ProfessorDTO?> ObterPorId(int id)
        {
            var professor = await _context.Professores
                //.Include(p => p.Cursos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professor == null) return null;

            return new ProfessorDTO
            {
                Id = professor.Id,
                Nome = professor.Nome,
                Email = professor.Email,
                //Cursos = professor.Cursos.Select(c => c.Nome).ToList()
            };
        }

        public async Task Criar(ProfessorDTO dto)
        {
            var professor = new Professor
            {
                Nome = dto.Nome,
                Email = dto.Email
            };

            _context.Professores.Add(professor);
            await _context.SaveChangesAsync();
        }

        public async Task Editar(ProfessorDTO dto)
        {
            var professor = await _context.Professores.FindAsync(dto.Id);
            if (professor == null) return;

            professor.Nome = dto.Nome;
            professor.Email = dto.Email;

            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            var professor = await _context.Professores.FindAsync(id);
            if (professor != null)
            {
                _context.Professores.Remove(professor);
                await _context.SaveChangesAsync();
            }
        }
    }
}