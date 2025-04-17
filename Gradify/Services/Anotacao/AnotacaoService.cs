using Gradify.Data;
using Gradify.Dto;
using Gradify.Services.Anotacao;

public class AnotacaoService : IAnotacaoInterface
{
    private readonly AppDbContext _context;

    public AnotacaoService(AppDbContext context)
    {
        _context = context;
    }

    public AnotacaoLeituraDto Criar(AnotacaoCriacaoDto dto)
    {
        var turma = _context.Turmas.Find(dto.TurmaId);
        if (turma == null) return null;

        var anotacao = new Gradify.Models.Anotacao
        {
            Comentario = dto.Comentario,
            TurmaId = dto.TurmaId
        };

        _context.Anotacoes.Add(anotacao);
        _context.SaveChanges();

        return new AnotacaoLeituraDto
        {
            Id = anotacao.Id,
            Comentario = anotacao.Comentario,
            Materia = turma.Materia 
        };
    }

    public IEnumerable<AnotacaoLeituraDto> GetAnotacoes()
    {
        return _context.Anotacoes
            .Join(_context.Turmas,
                  anotacao => anotacao.TurmaId,
                  turma => turma.Id,
                  (anotacao, turma) => new AnotacaoLeituraDto
                  {
                      Id = anotacao.Id,
                      Comentario = anotacao.Comentario,
                      Materia = turma.Materia
                  })
            .OrderBy(a => a.Materia)
            .ToList();
    }

    public AnotacaoLeituraDto ObterPorId(int id)
    {
        var anotacao = _context.Anotacoes.Find(id);
        if (anotacao == null) return null;

        var turma = _context.Turmas.Find(anotacao.TurmaId);
        var materia = turma != null ? turma.Materia : "Desconhecida";

        return new AnotacaoLeituraDto
        {
            Id = anotacao.Id,
            Comentario = anotacao.Comentario,
            Materia = materia
        };
    }

    public bool Excluir(int id)
    {
        var anotacao = _context.Anotacoes.Find(id); 
        if (anotacao == null) return false; 

        _context.Anotacoes.Remove(anotacao);
        _context.SaveChanges(); 
        return true; 
    }


    public AnotacaoLeituraDto Editar(int id, AnotacaoCriacaoDto dto)
    {
        var anotacao = _context.Anotacoes.Find(id);
        if (anotacao == null) return null;

        anotacao.Comentario = dto.Comentario;
        anotacao.TurmaId = dto.TurmaId;

        _context.Anotacoes.Update(anotacao);
        _context.SaveChanges();

        var turma = _context.Turmas.Find(dto.TurmaId);
        var materia = turma != null ? turma.Materia : "Desconhecida";

        return new AnotacaoLeituraDto
        {
            Id = anotacao.Id,
            Comentario = anotacao.Comentario,
            Materia = materia
        };
    }
}