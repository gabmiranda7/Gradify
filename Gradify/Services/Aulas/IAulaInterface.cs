using Gradify.DTOs;

namespace Gradify.Services.Aulas
{
    public interface IAulaInterface
    {
        Task<List<AulaDTO>> GetAulas();
        Task<AulaDTO?> ObterPorId(int id);
        Task Criar(AulaDTO dto);
        Task Editar(AulaDTO dto);
        Task Excluir(int id);
    }
}