using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
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

        public async Task<AnotacaoDto?> ObterPorId(int id)
        {
            try
            {
                var anotacao = await _context.Anotacoes.FindAsync(id);
                if (anotacao == null) return null;

                return new AnotacaoDto
                {
                    Id = anotacao.Id,
                    Texto = anotacao.Texto,
                    TurmaId = anotacao.TurmaId
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<AnotacaoDto>> GetAnotacoes()
        {
            try
            {
                return await _context.Anotacoes
                    .Select(a => new AnotacaoDto
                    {
                        Id = a.Id,
                        Texto = a.Texto,
                        TurmaId = a.TurmaId
                    })
                    .ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<AnotacaoDto>();
            }
        }

        public async Task<AnotacaoDto> Criar(AnotacaoDto anotacaoDto)
        {
            try
            {
                var anotacao = new Anotacao
                {
                    Texto = anotacaoDto.Texto,
                    TurmaId = anotacaoDto.TurmaId
                };

                _context.Anotacoes.Add(anotacao);
                await _context.SaveChangesAsync();

                anotacaoDto.Id = anotacao.Id;
                return anotacaoDto;
            }
            catch
            {
                return anotacaoDto;
            }
        }

        public async Task<AnotacaoDto?> Editar(int id, AnotacaoDto anotacaoDto)
        {
            try
            {
                var anotacao = await _context.Anotacoes.FindAsync(id);
                if (anotacao == null) return null;

                anotacao.Texto = anotacaoDto.Texto;
                anotacao.TurmaId = anotacaoDto.TurmaId;

                await _context.SaveChangesAsync();
                return anotacaoDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Excluir(int id)
        {
            try
            {
                var anotacao = await _context.Anotacoes.FindAsync(id);
                if (anotacao == null) return false;

                _context.Anotacoes.Remove(anotacao);
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