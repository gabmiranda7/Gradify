using Gradify.Dto;

namespace Gradify.Services.Turma
{
    public interface ITurmaInterface
    {
        TurmaLeituraDto Criar(TurmaCriacaoDto dto);
        TurmaLeituraDto Editar(int id, TurmaCriacaoDto dto);
        IEnumerable<TurmaLeituraDto> GetTurmas();
        TurmaLeituraDto ObterPorId(int id);
        bool Excluir(int id);
    }
}