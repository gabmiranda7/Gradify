using Gradify.DTOs;

namespace Gradify.Services.Alunos
{
    public interface IAlunoInterface
    {
        Task<AlunoDto?> ObterPorId(int id);
        Task<IEnumerable<AlunoDto>> GetAlunos();
        Task<AlunoDto> Criar(AlunoDto alunoDto);
        Task<AlunoDto?> Editar(int id, AlunoDto alunoDto);
        Task<bool> Excluir(int id);
    }
}