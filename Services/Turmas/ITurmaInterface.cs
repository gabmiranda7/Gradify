using Gradify.DTOs;

namespace Gradify.Services.Turmas
{
    public interface ITurmaInterface
    {
        Task<IEnumerable<TurmaDto>> GetTurmas();
        Task<TurmaDto?> ObterPorId(int id);
        Task<TurmaDto> Criar(TurmaDto turmaDto);
        Task<TurmaDto?> Editar(int id, TurmaDto turmaDto);
        Task<bool> Excluir(int id);
    }
}