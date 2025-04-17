using Gradify.Dto;

namespace Gradify.Services.Anotacao
{
    public interface IAnotacaoInterface
    {
        AnotacaoLeituraDto Criar(AnotacaoCriacaoDto dto);
        AnotacaoLeituraDto Editar(int id, AnotacaoCriacaoDto dto);
        IEnumerable<AnotacaoLeituraDto> GetAnotacoes();
        AnotacaoLeituraDto ObterPorId(int id);
        bool Excluir(int id);
    }
}