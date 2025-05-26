using Gradify.DTOs;

namespace Gradify.Services.Frequencia
{
    public interface IFrequenciaInterface
    {
        FrequenciaDto Criar(FrequenciaDto dto);
        FrequenciaDto Editar(int id, FrequenciaDto dto);
        IEnumerable<FrequenciaDto> GetFrequencias();
        FrequenciaDto ObterPorId(int id);
        bool Excluir(int id);
        List<FrequenciaDto> BuscarFrequenciasPorAluno(int alunoId);
    }
}