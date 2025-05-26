using Gradify.DTOs;

namespace Gradify.Services.Cursos
{
    public interface ICursoInterface
    {
        Task<IEnumerable<CursoDto>> GetCursos();
        Task<CursoDto?> ObterPorId(int id);
        Task<CursoDto?> Criar(CursoDto cursoDto);
        Task<CursoDto?> Editar(int id, CursoDto cursoDto);
        Task<bool> Excluir(int id);
    }
}