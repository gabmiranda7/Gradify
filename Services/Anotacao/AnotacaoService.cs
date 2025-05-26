using Gradify.Data;
using Gradify.DTOs;
using Gradify.Models;
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

    public AnotacaoDto Criar(AnotacaoDto dto)
    {
        var turma = _context.Turmas.Find(dto.TurmaId);
        if (turma == null) return null;

        var anotacao = new Anotacao
        {
            Texto = dto.Texto,
            TurmaId = dto.TurmaId,
            AlunoId = dto.AlunoId,
            DataCriacao = DateTime.Now
        };

        _context.Anotacoes.Add(anotacao);
        _context.SaveChanges();

        return new AnotacaoDto
        {
            Id = anotacao.Id,
            Texto = anotacao.Texto,
            TurmaId = anotacao.TurmaId,
            AlunoId = anotacao.AlunoId,
            DataCriacao = anotacao.DataCriacao
        };
    }

    public IEnumerable<AnotacaoDto> GetAnotacoes()
    {
        return _context.Anotacoes
            .Include(a => a.Turma)
            .OrderBy(a => a.DataCriacao)
            .Select(a => new AnotacaoDto
            {
                Id = a.Id,
                Texto = a.Texto,
                TurmaId = a.TurmaId,
                AlunoId = a.AlunoId,
                DataCriacao = a.DataCriacao
            })
            .ToList();
    }

    public AnotacaoDto ObterPorId(int id)
    {
        var anotacao = _context.Anotacoes
            .Include(a => a.Turma)
            .FirstOrDefault(a => a.Id == id);

        if (anotacao == null) return null;

        return new AnotacaoDto
        {
            Id = anotacao.Id,
            Texto = anotacao.Texto,
            TurmaId = anotacao.TurmaId,
            AlunoId = anotacao.AlunoId,
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

    public AnotacaoDto Editar(int id, AnotacaoDto dto)
    {
        var anotacao = _context.Anotacoes.Find(id);
        if (anotacao == null) return null;

        anotacao.Texto = dto.Texto;
        anotacao.TurmaId = dto.TurmaId;
        anotacao.AlunoId = dto.AlunoId;

        _context.Anotacoes.Update(anotacao);
        _context.SaveChanges();

        return new AnotacaoDto
        {
            Id = anotacao.Id,
            Texto = anotacao.Texto,
            TurmaId = anotacao.TurmaId,
            AlunoId = anotacao.AlunoId,
            DataCriacao = anotacao.DataCriacao
        };
    }
}