using Gradify.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gradify.Services.Professores
{
    public interface IProfessorInterface
    {
        Task<List<ProfessorDTO>> GetProfessores();
        Task<ProfessorDTO?> ObterPorId(int id);
        Task Criar(ProfessorDTO dto);
        Task Editar(ProfessorDTO dto);
        Task Excluir(int id);
    }
}