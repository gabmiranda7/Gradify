using Gradify.DTOs;

namespace Gradify.Services.Frequencias
{
    public interface IFrequenciaInterface
    {
        Task<IEnumerable<FrequenciaDto>> GetFrequencias();
        Task<FrequenciaDto?> ObterPorId(int id);
        Task<FrequenciaDto> Criar(FrequenciaDto dto);
        Task<FrequenciaDto?> Editar(int id, FrequenciaDto dto);
        Task<bool> Excluir(int id);
    }
}