using Gradify.DTOs;

namespace Gradify.Services.Anotacoes
{
    public interface IAnotacaoInterface
    {
        Task<AnotacaoDTO?> ObterPorAulaId(int aulaId);
        Task SalvarAnotacao(AnotacaoDTO dto);
    }
}
