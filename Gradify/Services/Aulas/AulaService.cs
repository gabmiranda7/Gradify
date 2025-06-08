using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Services.Aulas
{
    public class AulaService : IAulaInterface
    {
        private readonly AppDbContext _context;

        public AulaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AulaDTO>> GetAulas()
        {
            return await _context.Aulas
                .Select(a => new AulaDTO
                {
                    Id = a.Id,
                    DataAula = a.DataAula,
                    HoraInicio = a.HoraInicio,
                    HoraFim = a.HoraFim,
                    CursoId = a.CursoId
                })
                .ToListAsync();
        }

        public async Task<AulaDTO?> ObterPorId(int id)
        {
            var aula = await _context.Aulas.FindAsync(id);
            if (aula == null) return null;

            return new AulaDTO
            {
                Id = aula.Id,
                DataAula = aula.DataAula,
                HoraInicio = aula.HoraInicio,
                HoraFim = aula.HoraFim,
                CursoId = aula.CursoId
            };
        }

        public async Task Criar(AulaDTO dto)
        {
            var aula = new Aula
            {
                DataAula = dto.DataAula,
                HoraInicio = dto.HoraInicio,
                HoraFim = dto.HoraFim,
                CursoId = dto.CursoId
            };

            _context.Aulas.Add(aula);
            await _context.SaveChangesAsync();
        }

        public async Task Editar(AulaDTO dto)
        {
            var aula = await _context.Aulas.FindAsync(dto.Id);
            if (aula == null) return;

            aula.DataAula = dto.DataAula;
            aula.HoraInicio = dto.HoraInicio;
            aula.HoraFim = dto.HoraFim;
            aula.CursoId = dto.CursoId;

            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            var aula = await _context.Aulas.FindAsync(id);
            if (aula != null)
            {
                _context.Aulas.Remove(aula);
                await _context.SaveChangesAsync();
            }
        }
    }
}