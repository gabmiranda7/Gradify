using Gradify.Dto;

namespace Gradify.Services.Professor
{
    public interface IProfessorInterface
    {
        ProfessorLeituraDto Criar(ProfessorCriacaoDto dto);
        ProfessorLeituraDto Editar(int id, ProfessorCriacaoDto dto);
        IEnumerable<ProfessorLeituraDto> GetProfessores();
        ProfessorLeituraDto ObterPorId(int id);
        bool Excluir(int id);
    }
}