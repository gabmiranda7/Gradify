using Gradify.DTOs;

namespace Gradify.Services.Aulas
{
    public interface IAulaInterface
    {
        Task<IEnumerable<AulaDto>> GetAulas();
        Task<AulaDto?> ObterPorId(int id);
        Task<AulaDto?> Criar(AulaDto aulaDto);
        Task<AulaDto?> Editar(int id, AulaDto aulaDto);
        Task<bool> Excluir(int id);
    }
}