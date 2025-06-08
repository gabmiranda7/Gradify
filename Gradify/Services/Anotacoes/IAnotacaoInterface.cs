using Gradify.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gradify.Services.Anotacoes
{
    public interface IAnotacaoInterface
    {
        Task<List<AnotacaoDTO>> GetAnotacoes();
        Task<List<AnotacaoDTO>> GetAnotacoesPorAlunoId(int alunoId);
        Task<List<SelectListItem>> GetCursosSelectList();
        Task<AnotacaoDTO?> ObterPorId(int id);
        Task Criar(AnotacaoDTO dto);
        Task Editar(AnotacaoDTO dto);
        Task Excluir(int id);
    }
}