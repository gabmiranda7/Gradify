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

        public async Task<IEnumerable<AlunoDto>> GetAlunos()
        {
            try
            {
                return await _context.Alunos
                    .Select(a => new AlunoDto
                    {
                        Id = a.Id,
                        Nome = a.Nome,
                        Matricula = a.Matricula,
                        Email = a.Email,
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar a lista de alunos.", ex);
            }
        }

        public async Task<AlunoDto?> ObterPorId(int id)
        {
            try
            {
                var aluno = await _context.Alunos.FindAsync(id);
                if (aluno == null) return null;

                return new AlunoDto
                {
                    Id = aluno.Id,
                    Nome = aluno.Nome,
                    Matricula = aluno.Matricula,
                    Email = aluno.Email,
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao obter aluno com ID {id}.", ex);
            }
        }

        public async Task<AlunoDto> Criar(AlunoDto alunoDto)
        {
            try
            {
                var aluno = new Aluno
                {
                    Nome = alunoDto.Nome,
                    Matricula = alunoDto.Matricula,
                    Email = alunoDto.Email,
                };

                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();

                alunoDto.Id = aluno.Id;
                return alunoDto;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao criar aluno.", ex);
            }
        }

        public async Task<AlunoDto?> Editar(int id, AlunoDto alunoDto)
        {
            try
            {
                var aluno = await _context.Alunos.FindAsync(id);
                if (aluno == null) return null;

                aluno.Nome = alunoDto.Nome;
                aluno.Matricula = alunoDto.Matricula;
                aluno.Email = alunoDto.Email;

                await _context.SaveChangesAsync();
                return alunoDto;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao editar aluno com ID {id}.", ex);
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                var aluno = await _context.Alunos.FindAsync(id);
                if (aluno == null) return false;

                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao excluir aluno com ID {id}.", ex);
            }
        }
    }
}