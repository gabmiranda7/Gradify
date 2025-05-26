using Gradify.DTOs;

namespace Gradify.Services.Anotacao
{
    public interface IAnotacaoInterface
    {
        AnotacaoDto Criar(AnotacaoDto dto);
        AnotacaoDto Editar(int id, AnotacaoDto dto);
        IEnumerable<AnotacaoDto> GetAnotacoes();
        AnotacaoDto ObterPorId(int id);
        bool Excluir(int id);
    }
}