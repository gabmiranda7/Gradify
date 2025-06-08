using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Gradify.Services.Frequencias;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gradify.Services.Frequencias
{
    public class FrequenciaService : IFrequenciaInterface
    {
        private readonly AppDbContext _context;

        public FrequenciaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FrequenciaDTO>> GetFrequencias()
        {
            return await _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .Select(f => new FrequenciaDTO
                {
                    Id = f.Id,
                    DataFrequencia = f.DataFrequencia,
                    AlunoId = f.AlunoId,
                    NomeAluno = f.Aluno.Nome,
                    TurmaId = f.TurmaId,
                    NomeTurma = f.Turma.Nome
                })
                .ToListAsync();
        }
        public async Task<FrequenciaDTO?> ObterPorId(int id)
        {
            var freq = await _context.Frequencias
                .Include(f => f.Aluno)
                .Include(f => f.Turma)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (freq == null) return null;

            return new FrequenciaDTO
            {
                Id = freq.Id,
                DataFrequencia = freq.DataFrequencia,
                AlunoId = freq.AlunoId,
                NomeAluno = freq.Aluno.Nome,
                TurmaId = freq.TurmaId,
                NomeTurma = freq.Turma.Nome
            };
        }

        public async Task Criar(FrequenciaDTO dto)
        {
            var frequencia = new Frequencia
            {
                DataFrequencia = dto.DataFrequencia,
                AlunoId = dto.AlunoId,
                TurmaId = dto.TurmaId
            };

            _context.Frequencias.Add(frequencia);
            await _context.SaveChangesAsync();
        }

        public async Task Editar(FrequenciaDTO dto)
        {
            var frequencia = await _context.Frequencias.FindAsync(dto.Id);
            if (frequencia == null) return;

            frequencia.DataFrequencia = dto.DataFrequencia;
            frequencia.AlunoId = dto.AlunoId;
            frequencia.TurmaId = dto.TurmaId;

            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            var frequencia = await _context.Frequencias.FindAsync(id);
            if (frequencia != null)
            {
                _context.Frequencias.Remove(frequencia);
                await _context.SaveChangesAsync();
            }
        }
    }
}