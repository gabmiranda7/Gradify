using Gradify.Data;
using Gradify.DTOs;
using Gradify.Enums;
using Gradify.Models;
using Gradify.Services.Professores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

public class ProfessorTests
{
    private readonly ITestOutputHelper _output;

    public ProfessorTests(ITestOutputHelper output)
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

        for (int i = 1; i <= 5; i++)
        {
            var usuario = new Usuario
            {
                UserName = $"professor{i}_{uniqueSuffix}",
                Email = $"professor{i}_{uniqueSuffix}@example.com",
                NormalizedUserName = $"PROFESSOR{i}_{uniqueSuffix}".ToUpper(),
                NormalizedEmail = $"PROFESSOR{i}_{uniqueSuffix}@EXAMPLE.COM".ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            context.Users.Add(usuario);
            await context.SaveChangesAsync();

            var professor = new Professor
            {
                Nome = $"Professor Teste {i}",
                UsuarioId = usuario.Id
            };

            context.Professores.Add(professor);
            await context.SaveChangesAsync();
        }

        var totalProfessores = await context.Professores.CountAsync();
        Assert.True(totalProfessores >= 5, "Deveria existir pelo menos 5 professores no banco.");
    }

    [Fact]
    public async Task Editar()
    {
        using var context = GetSqlServerDbContext();
        var service = new ProfessorService(context);

        var professores = await context.Professores.OrderBy(p => p.Id).Take(10).ToListAsync();

        if (professores.Count < 10)
        {
            int faltam = 10 - professores.Count;
            for (int i = 1; i <= faltam; i++)
            {
                var usuario = new Usuario
                {
                    UserName = $"prof_edit_{Guid.NewGuid().ToString().Substring(0, 8)}",
                    Email = $"prof_edit_{Guid.NewGuid().ToString().Substring(0, 8)}@example.com",
                    NormalizedUserName = $"PROF_EDIT_{Guid.NewGuid().ToString().Substring(0, 8)}".ToUpper(),
                    NormalizedEmail = $"PROF_EDIT_{Guid.NewGuid().ToString().Substring(0, 8)}@EXAMPLE.COM".ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                context.Users.Add(usuario);
                await context.SaveChangesAsync();

                var professor = new Professor
                {
                    Nome = $"Professor Teste Editar {i}",
                    UsuarioId = usuario.Id
                };

                context.Professores.Add(professor);
                await context.SaveChangesAsync();
            }

            professores = await context.Professores.OrderBy(p => p.Id).Take(10).ToListAsync();
        }

        Assert.Equal(10, professores.Count);

        int idx = 1;
        foreach (var professor in professores)
        {
            var dto = new ProfessorDTO
            {
                Id = professor.Id,
                Nome = $"Professor Editado {idx}",
                Email = professor.Usuario?.Email ?? $"editado{idx}@exemplo.com",
                Senha = "NovaSenha123!",
                Tipo = TipoUsuario.Professor
            };

            await service.Editar(dto);

            var atualizado = await context.Professores.FindAsync(professor.Id);
            Assert.Equal($"Professor Editado {idx}", atualizado?.Nome);

            idx++;
        }
    }

    [Fact]
    public async Task ListarTodos()
    {
        using var context = GetSqlServerDbContext();
        var service = new ProfessorService(context);

        var lista = await service.GetProfessores();

        Assert.NotNull(lista);

        _output.WriteLine($"Total de professores: {lista.Count}");

        if (lista.Count == 0)
        {
            _output.WriteLine("Nenhum professor encontrado.");
        }
        else
        {
            foreach (var professor in lista)
            {
                _output.WriteLine($"Professor: {professor.Id} - {professor.Nome}");
            }
        }
    }

    [Fact]
    public async Task ExcluirTodos()
    {
        using var context = GetSqlServerDbContext();

        var totalAntes = await context.Professores.CountAsync();
        Console.WriteLine($"Total de professores antes da exclusão: {totalAntes}");

        var professoresSemCurso = await context.Professores
            //.Where(p => !context.Cursos.Any(c => c.ProfessorId == p.Id))
            .ToListAsync();

        context.Professores.RemoveRange(professoresSemCurso);
        await context.SaveChangesAsync();

        var totalDepois = await context.Professores.CountAsync();
        Console.WriteLine($"Total de professores depois da exclusão: {totalDepois}");

        // Verifica se algum dos IDs excluídos ainda existe no banco
        var existeAlgumExcluido = await context.Professores
            .AnyAsync(p => professoresSemCurso.Select(x => x.Id).Contains(p.Id));

        Assert.False(existeAlgumExcluido, "Algum professor excluído ainda existe no banco.");

        if (totalDepois == 0)
        {
            await context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Professores', RESEED, 0)");
            Console.WriteLine("Todos os professores foram excluídos e o Identity foi resetado.");
        }
        else
        {
            Console.WriteLine("Somente os professores sem curso foram excluídos.");
        }
    }
}