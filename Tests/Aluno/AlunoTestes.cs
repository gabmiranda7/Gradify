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
    public async Task Criar()
    {
        using var context = GetSqlServerDbContext();

        var uniqueSuffix = GetUniqueSuffix();

        var usuario = new Usuario
        {
            UserName = $"aluno_{uniqueSuffix}",
            Email = $"aluno_{uniqueSuffix}@example.com",
            NormalizedUserName = $"ALUNO_{uniqueSuffix}".ToUpper(),
            NormalizedEmail = $"ALUNO_{uniqueSuffix}@EXAMPLE.COM".ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        context.Users.Add(usuario);
        await context.SaveChangesAsync();

        var aluno = new Aluno
        {
            Nome = "Aluno Teste",
            Matricula = "123456",
            UsuarioId = usuario.Id
        };

        context.Alunos.Add(aluno);
        await context.SaveChangesAsync();

        var alunoCarregado = await context.Alunos
            .Include(a => a.Usuario)
            .FirstOrDefaultAsync(a => a.Id == aluno.Id);

        Assert.NotNull(alunoCarregado);
        Assert.NotNull(alunoCarregado.Usuario);
        Assert.Equal("Aluno Teste", alunoCarregado.Nome);
        Assert.Equal(usuario.Email, alunoCarregado.Usuario.Email);

        _output.WriteLine($"Aluno criado: {alunoCarregado.Nome}, Email: {alunoCarregado.Usuario.Email}");
    }

    [Fact]
    public async Task Editar()
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
    public async Task ListarTodos()
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
    public async Task ExcluirTodos()
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