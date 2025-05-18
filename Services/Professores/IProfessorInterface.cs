using Gradify.DTOs;

namespace Gradify.Services.Professores
{
    public interface IProfessorInterface
    {
        Task<ProfessorDto?> ObterPorId(int id);
        Task<IEnumerable<ProfessorDto>> GetProfessores();
        Task<ProfessorDto> Criar(ProfessorDto professorDto);
        Task<ProfessorDto?> Editar(int id, ProfessorDto professorDto);
        Task<bool> Excluir(int id);
    }
}