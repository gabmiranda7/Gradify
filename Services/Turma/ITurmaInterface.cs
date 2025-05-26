using Gradify.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gradify.Services.Turma
{
    public interface ITurmaInterface
    {
        Task<TurmaDto> Criar(TurmaDto dto);
        Task<TurmaDto> Editar(int id, TurmaDto dto);
        Task<IEnumerable<TurmaDto>> GetTurmas();
        Task<TurmaDto> ObterPorId(int id);
        Task<bool> Excluir(int id);
    }
}