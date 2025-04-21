using Gradify.Dto;

namespace Gradify.Services.Aluno
{
    public interface IAlunoInterface
    {
        AlunoLeituraDto Criar(AlunoCriacaoDto dto);
        AlunoLeituraDto Editar(int id, AlunoCriacaoDto dto);
        IEnumerable<AlunoLeituraDto> GetAlunos();
        AlunoLeituraDto ObterPorId(int id);
        bool Excluir(int id);
    }
}