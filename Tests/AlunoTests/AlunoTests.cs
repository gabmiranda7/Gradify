using Xunit;
using Microsoft.EntityFrameworkCore;
using Gradify.Data;
using Gradify.Models;
using Gradify.Services.Alunos;
using System.Linq;
using System.Threading.Tasks;
using System;

public class AlunoTests
{
    private AppDbContext GetSqlServerDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=GABRIEL\\SQLEXPRESS;Database=Gradify;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=120;")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CriarAlunos()
    {
        using var context = GetSqlServerDbContext();
        var service = new AlunoService(context);

        for (int i = 1; i <= 10; i++)
        {
            var aluno = new Gradify.DTOs.AlunoDto
            {
                Nome = $"Aluno {i}",
                Matricula = $"MAT{i:D3}",
                Email = $"aluno{i}@exemplo.com"
            };
            await service.Criar(aluno);
        }

        var total = await context.Alunos.CountAsync();
        Assert.True(total >= 10, $"Esperava pelo menos 10 alunos, mas há {total}.");
    }

    [Fact]
    public async Task EditarAlunos()
    {
        using var context = GetSqlServerDbContext();
        var service = new AlunoService(context);

        var alunos = context.Alunos.OrderBy(a => a.Id).Take(10).ToList();

        Assert.True(alunos.Count == 10, $"Esperava 10 alunos para editar, mas há {alunos.Count}.");

        int i = 1;
        foreach (var aluno in alunos)
        {
            var alunoEditado = new Gradify.DTOs.AlunoDto
            {
                Nome = $"Aluno Editado {i}",
                Matricula = $"EDIT{i:D3}",
                Email = $"editado{i}@exemplo.com"
            };

            var resultado = await service.Editar(aluno.Id, alunoEditado);

            Assert.NotNull(resultado);
            Assert.Equal($"Aluno Editado {i}", resultado.Nome);

            i++;
        }
    }

    [Fact]
    public async Task ListarAlunos()
    {
        using var context = GetSqlServerDbContext();
        var service = new AlunoService(context);

        var lista = await service.GetAlunos();

        Assert.NotNull(lista);
        Assert.True(lista.Count() >= 10, $"Esperava pelo menos 10 alunos, mas há {lista.Count()}.");

        foreach (var aluno in lista)
        {
            System.Diagnostics.Debug.WriteLine($"Aluno: {aluno.Id} - {aluno.Nome} - {aluno.Matricula}");
        }

        Console.WriteLine($"Total de alunos: {lista.Count()}");
    }

    [Fact]
    public async Task ExcluirTodosAlunos()
    {
        using var context = GetSqlServerDbContext();

        var todos = context.Alunos.ToList();

        context.Alunos.RemoveRange(todos);
        await context.SaveChangesAsync();

        var total = await context.Alunos.CountAsync();

        Assert.Equal(0, total);

        var sql = "DBCC CHECKIDENT ('Alunos', RESEED, 0)";
        await context.Database.ExecuteSqlRawAsync(sql);

        System.Diagnostics.Debug.WriteLine("Todos os alunos foram excluídos e Identity resetado.");
    }
}