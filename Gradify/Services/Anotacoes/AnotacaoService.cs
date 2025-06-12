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

        public async Task<AnotacaoDTO?> ObterPorAulaId(int aulaId)
        {
            var aula = await _context.Aulas.FirstOrDefaultAsync(a => a.Id == aulaId);
            if (aula == null) return null;

            var anotacao = await _context.Anotacoes.FirstOrDefaultAsync(a => a.AulaId == aulaId);

            return new AnotacaoDTO
            {
                AulaId = aula.Id,
                Tema = aula.Tema,
                DataAula = aula.DataAula,
                Texto = anotacao?.Texto ?? string.Empty
            };
        }

        public async Task SalvarAnotacao(AnotacaoDTO dto)
        {
            var anotacao = await _context.Anotacoes.FirstOrDefaultAsync(a => a.AulaId == dto.AulaId);

            if (anotacao == null)
            {
                anotacao = new Anotacao
                {
                    AulaId = dto.AulaId,
                    Texto = dto.Texto
                };
                _context.Anotacoes.Add(anotacao);
            }
            else
            {
                anotacao.Texto = dto.Texto;
                _context.Anotacoes.Update(anotacao);
            }

            await _context.SaveChangesAsync();
        }
    }
}
