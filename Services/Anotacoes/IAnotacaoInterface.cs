using Gradify.DTOs;

namespace Gradify.Services.Anotacoes
{
    public interface IAnotacaoInterface
    {
        Task<AnotacaoDto?> ObterPorId(int id);
        Task<IEnumerable<AnotacaoDto>> GetAnotacoes();
        Task<AnotacaoDto> Criar(AnotacaoDto anotacaoDto);
        Task<AnotacaoDto?> Editar(int id, AnotacaoDto anotacaoDto);
        Task<bool> Excluir(int id);
    }
}
