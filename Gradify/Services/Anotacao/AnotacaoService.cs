using Gradify.Data;
using Gradify.Dto;
using Gradify.Services.Anotacao;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            TurmaId = dto.TurmaId,
            DataCriacao = DateTime.Now
        };

        _context.Anotacoes.Add(anotacao);
        _context.SaveChanges();

        return new AnotacaoLeituraDto
        {
            Id = anotacao.Id,
            Comentario = anotacao.Comentario,
            Materia = turma.Materia,
            TurmaId = dto.TurmaId,
            DataCriacao = anotacao.DataCriacao
        };
    }

    public IEnumerable<AnotacaoLeituraDto> GetAnotacoes()
    {
        return _context.Anotacoes
            .OrderBy(a => a.DataCriacao)
            .Select(a => new AnotacaoLeituraDto
            {
                Id = a.Id,
                Comentario = a.Comentario,
                Materia = a.Turma.Materia,
                DataCriacao = a.DataCriacao,
            })
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
            Materia = materia,
            TurmaId = anotacao.TurmaId,
            DataCriacao = anotacao.DataCriacao
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
            Materia = materia,
            TurmaId = dto.TurmaId,
            DataCriacao = anotacao.DataCriacao
        };
    }
}