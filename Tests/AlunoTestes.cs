using Gradify.Data;
using Gradify.DTOs;
using Gradify.Enums;
using Gradify.Models;
using Gradify.Services.Alunos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

public class AlunoTests
{
    private readonly ITestOutputHelper _output;

    public AlunoTests(ITestOutputHelper output)
    {
        _output = output;
    }

    private AppDbContext GetSqlServerDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=GABRIEL\\SQLEXPRESS;Database=Gradify;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=120;")
            .Options;

        return new AppDbContext(options);
    }

    private string GetUniqueSuffix()
    {
        return "test" + DateTime.UtcNow.Ticks.ToString();
    }

    [Fact]
    public async Task CriarCincoAlunos()
    {
        using var context = GetSqlServerDbContext();
        var uniqueSuffix = GetUniqueSuffix();

        for (int i = 1; i <= 5; i++)
        {
            var usuario = new Usuario
            {
                UserName = $"aluno{i}_{uniqueSuffix}",
                Email = $"aluno{i}_{uniqueSuffix}@example.com",
                NormalizedUserName = $"ALUNO{i}_{uniqueSuffix}".ToUpper(),
                NormalizedEmail = $"ALUNO{i}_{uniqueSuffix}@EXAMPLE.COM".ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            context.Users.Add(usuario);
            await context.SaveChangesAsync();

            var aluno = new Aluno
            {
                Nome = $"Aluno Teste {i}",
                Matricula = $"12345{i}",
                UsuarioId = usuario.Id
            };

            context.Alunos.Add(aluno);
            await context.SaveChangesAsync();
        }

        var totalAlunos = await context.Alunos.CountAsync();
        Assert.True(totalAlunos >= 5, "Deveria existir pelo menos 5 alunos no banco.");
    }

    [Fact]
    public async Task EditarAlunos()
    {
        using var context = GetSqlServerDbContext();
        var service = new AlunoService(context);

        var alunos = await context.Alunos.OrderBy(a => a.Id).Take(10).ToListAsync();

        if (alunos.Count < 10)
        {
            int faltam = 10 - alunos.Count;
            for (int i = 1; i <= faltam; i++)
            {
                var usuario = new Usuario
                {
                    UserName = $"aluno_edit_{Guid.NewGuid().ToString().Substring(0, 8)}",
                    Email = $"aluno_edit_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com",
                    NormalizedUserName = $"ALUNO_EDIT_{Guid.NewGuid().ToString().Substring(0, 8)}".ToUpper(),
                    NormalizedEmail = $"ALUNO_EDIT_{Guid.NewGuid().ToString().Substring(0, 8)}@EXAMPLE.COM".ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                context.Users.Add(usuario);
                await context.SaveChangesAsync();

                var aluno = new Aluno
                {
                    Nome = $"Aluno Teste Editar {i}",
                    Matricula = $"EDITAR123{i}",
                    UsuarioId = usuario.Id
                };

                context.Alunos.Add(aluno);
                await context.SaveChangesAsync();
            }

            alunos = await context.Alunos.OrderBy(a => a.Id).Take(10).ToListAsync();
        }

        Assert.Equal(10, alunos.Count);

        int idx = 1;
        foreach (var aluno in alunos)
        {
            var dto = new AlunoDTO
            {
                Id = aluno.Id,
                Nome = $"Aluno Editado {idx}",
                Matricula = $"EDIT{idx:D3}",
                Email = aluno.Usuario?.Email ?? $"editado{idx}@exemplo.com",
                Senha = "NovaSenha123!",
                Tipo = TipoUsuario.Aluno,
                TurmaId = aluno.TurmaId ?? 0
            };

            await service.Editar(dto);

            var atualizado = await context.Alunos.FindAsync(aluno.Id);
            Assert.Equal($"Aluno Editado {idx}", atualizado?.Nome);

            idx++;
        }
    }

    [Fact]
    public async Task ListarAlunos()
    {
        using var context = GetSqlServerDbContext();
        var service = new AlunoService(context);

        var lista = await service.GetAlunos();

        Assert.NotNull(lista);

        _output.WriteLine($"Total de alunos: {lista.Count}");

        if (lista.Count == 0)
        {
            _output.WriteLine("Nenhum aluno encontrado.");
        }
        else
        {
            foreach (var aluno in lista)
            {
                _output.WriteLine($"Aluno: {aluno.Id} - {aluno.Nome} - {aluno.Matricula}");
            }
        }
    }


    [Fact]
    public async Task ExcluirTodosAlunos()
    {
        using var context = GetSqlServerDbContext();

        var totalAntes = await context.Alunos.CountAsync();
        Console.WriteLine($"Total de alunos antes da exclusão: {totalAntes}");

        var alunos = await context.Alunos.ToListAsync();

        context.Alunos.RemoveRange(alunos);
        await context.SaveChangesAsync();

        var totalDepois = await context.Alunos.CountAsync();
        Console.WriteLine($"Total de alunos depois da exclusão: {totalDepois}");

        Assert.Equal(0, totalDepois);

        await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Alunos', RESEED, 0)");

        Console.WriteLine("Todos os alunos foram excluídos e o Identity foi resetado.");
    }
}