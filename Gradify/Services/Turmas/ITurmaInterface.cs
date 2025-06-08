using Gradify.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gradify.Services.Turmas
{
    public interface ITurmaInterface
    {
        Task<List<TurmaDTO>> GetTurmas();
        Task<TurmaDTO?> ObterPorId(int id);
        Task Criar(TurmaDTO dto);
        Task Editar(TurmaDTO dto);
        Task Excluir(int id);
    }
}