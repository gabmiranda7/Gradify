using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Services.Alunos
{
    public class AlunoService : IAlunoInterface
    {
        private readonly AppDbContext _context;

        public AlunoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlunoDTO>> GetAlunos()
        {
            return await _context.Alunos
                .Select(a => new AlunoDTO
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Matricula = a.Matricula,
                    Email = a.Email
                })
                .ToListAsync();
        }

        public async Task<AlunoDTO?> ObterPorId(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Frequencias)
                    .ThenInclude(f => f.Turma)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (aluno == null) return null;

            return new AlunoDTO
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Matricula = aluno.Matricula,
                Email = aluno.Email,
                Frequencias = aluno.Frequencias.Select(f => new FrequenciaDTO
                {
                    Id = f.Id,
                    DataFrequencia = f.DataFrequencia,
                    TurmaId = f.TurmaId,
                    NomeTurma = f.Turma.Nome
                }).ToList()
            };
        }

        public async Task Criar(AlunoDTO dto)
        {
            var aluno = new Aluno
            {
                Nome = dto.Nome,
                Matricula = dto.Matricula,
                Email = dto.Email
            };
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task Editar(AlunoDTO dto)
        {
            var aluno = await _context.Alunos.FindAsync(dto.Id);
            if (aluno == null) return;

            aluno.Nome = dto.Nome;
            aluno.Matricula = dto.Matricula;
            aluno.Email = dto.Email;

            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
        }
    }
}