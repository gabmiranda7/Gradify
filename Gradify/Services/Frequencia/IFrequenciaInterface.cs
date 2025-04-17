using Gradify.Dto;

namespace Gradify.Services.Frequencia
{
    public interface IFrequenciaInterface
    {
        FrequenciaLeituraDto Criar(FrequenciaCriacaoDto dto);
        FrequenciaLeituraDto Editar(int id, FrequenciaCriacaoDto dto);
        IEnumerable<FrequenciaLeituraDto> GetFrequencias();
        FrequenciaLeituraDto ObterPorId(int id);
        bool Excluir(int id);
    }
}
