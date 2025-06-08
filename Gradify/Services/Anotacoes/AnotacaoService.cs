using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Services.Anotacoes
{
    public class AnotacaoService : IAnotacaoInterface
    {
        private readonly AppDbContext _context;

        public AnotacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AnotacaoDTO>> GetAnotacoes()
        {
            return await _context.Anotacoes
                .Include(a => a.Aluno)
                .Include(a => a.Curso)
                .Select(a => new AnotacaoDTO
                {
                    Id = a.Id,
                    Texto = a.Texto,
                    DataCriacao = a.DataCriacao,
                    DataModificacao = a.DataModificacao,
                    AlunoId = a.AlunoId,
                    CursoId = a.CursoId,
                    AlunoNome = a.Aluno.Nome,
                    CursoNome = a.Curso.Nome
                }).ToListAsync();
        }

        public async Task<List<AnotacaoDTO>> GetAnotacoesPorAlunoId(int alunoId)
        {
            return await _context.Anotacoes
                .Where(a => a.AlunoId == alunoId)
                .Include(a => a.Curso)
                .Include(a => a.Aluno)
                .Select(a => new AnotacaoDTO
                {
                    Id = a.Id,
                    Texto = a.Texto,
                    DataCriacao = a.DataCriacao,
                    DataModificacao = a.DataModificacao,
                    AlunoId = a.AlunoId,
                    AlunoNome = a.Aluno.Nome,
                    CursoId = a.CursoId,
                    CursoNome = a.Curso.Nome
                }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetCursosSelectList()
        {
            return await _context.Cursos
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nome
                })
                .ToListAsync();
        }

        public async Task<AnotacaoDTO?> ObterPorId(int id)
        {
            var anotacao = await _context.Anotacoes
                .Include(a => a.Aluno)
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (anotacao == null) return null;

            return new AnotacaoDTO
            {
                Id = anotacao.Id,
                Texto = anotacao.Texto,
                DataCriacao = anotacao.DataCriacao,
                DataModificacao = anotacao.DataModificacao,
                AlunoId = anotacao.AlunoId,
                CursoId = anotacao.CursoId,
                AlunoNome = anotacao.Aluno.Nome,
                CursoNome = anotacao.Curso.Nome
            };
        }

        public async Task Criar(AnotacaoDTO dto)
        {
            var anotacao = new Gradify.Models.Anotacao
            {
                Texto = dto.Texto,
                DataCriacao = dto.DataCriacao,
                DataModificacao = dto.DataModificacao,
                AlunoId = dto.AlunoId,
                CursoId = dto.CursoId
            };

            _context.Anotacoes.Add(anotacao);
            await _context.SaveChangesAsync();
        }

        public async Task Editar(AnotacaoDTO dto)
        {
            var anotacao = await _context.Anotacoes.FindAsync(dto.Id);
            if (anotacao == null) return;

            anotacao.Texto = dto.Texto;
            anotacao.DataCriacao = dto.DataCriacao;
            anotacao.DataModificacao = dto.DataModificacao;
            anotacao.AlunoId = dto.AlunoId;
            anotacao.CursoId = dto.CursoId;

            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            var anotacao = await _context.Anotacoes.FindAsync(id);
            if (anotacao != null)
            {
                _context.Anotacoes.Remove(anotacao);
                await _context.SaveChangesAsync();
            }
        }
    }
}