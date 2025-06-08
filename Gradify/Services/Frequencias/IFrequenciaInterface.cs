using Gradify.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gradify.Services.Frequencias
{
    public interface IFrequenciaInterface
    {
        Task<List<FrequenciaDTO>> GetFrequencias();
        Task<FrequenciaDTO?> ObterPorId(int id);
        Task Criar(FrequenciaDTO dto);
        Task Editar(FrequenciaDTO dto);
        Task Excluir(int id);
    }
}