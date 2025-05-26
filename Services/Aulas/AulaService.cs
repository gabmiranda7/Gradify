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

        public async Task<IEnumerable<AulaDto>> GetAulas()
        {
            return await _context.Aulas
                .Select(a => new AulaDto
                {
                    Id = a.Id,
                    DataAula = a.DataAula,
                    HoraInicioChamada = a.HoraInicioChamada,
                    HoraFimChamada = a.HoraFimChamada,
                    TurmaId = a.TurmaId
                }).ToListAsync();
        }

        public async Task<AulaDto?> ObterPorId(int id)
        {
            var aula = await _context.Aulas.FindAsync(id);
            if (aula == null) return null;

            return new AulaDto
            {
                Id = aula.Id,
                DataAula = aula.DataAula,
                HoraInicioChamada = aula.HoraInicioChamada,
                HoraFimChamada = aula.HoraFimChamada,
                TurmaId = aula.TurmaId
            };
        }

        public async Task<AulaDto?> Criar(AulaDto aulaDto)
        {
            var aula = new Aula
            {
                DataAula = aulaDto.DataAula,
                HoraInicioChamada = aulaDto.HoraInicioChamada,
                HoraFimChamada = aulaDto.HoraFimChamada,
                TurmaId = aulaDto.TurmaId
            };

            _context.Aulas.Add(aula);
            await _context.SaveChangesAsync();

            aulaDto.Id = aula.Id;
            return aulaDto;
        }

        public async Task<AulaDto?> Editar(int id, AulaDto aulaDto)
        {
            var aula = await _context.Aulas.FindAsync(id);
            if (aula == null) return null;

            aula.DataAula = aulaDto.DataAula;
            aula.HoraInicioChamada = aulaDto.HoraInicioChamada;
            aula.HoraFimChamada = aulaDto.HoraFimChamada;
            aula.TurmaId = aulaDto.TurmaId;

            await _context.SaveChangesAsync();

            return aulaDto;
        }

        public async Task<bool> Excluir(int id)
        {
            var aula = await _context.Aulas.FindAsync(id);
            if (aula == null) return false;

            _context.Aulas.Remove(aula);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}