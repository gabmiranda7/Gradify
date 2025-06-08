using Gradify.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gradify.Services.Alunos
{
    public interface IAlunoInterface
    {
        Task<List<AlunoDTO>> GetAlunos();
        Task<AlunoDTO?> ObterPorId(int id);
        Task Criar(AlunoDTO dto);
        Task Editar(AlunoDTO dto);
        Task Excluir(int id);
    }
}