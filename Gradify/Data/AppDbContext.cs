using Gradify.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Data
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Anotacao> Anotacoes { get; set; }
        public DbSet<Frequencia> Frequencias { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Turma> Turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Anotacao>()
                .HasOne(a => a.Turma)
                .WithMany(t => t.Anotacoes)
                .HasForeignKey(a => a.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Frequencia>()
                .HasOne(f => f.Aluno)
                .WithMany(a => a.Frequencias)
                .HasForeignKey(f => f.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Frequencia>()
                .HasOne(f => f.Turma)
                .WithMany(t => t.Frequencias)
                .HasForeignKey(f => f.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Turma>()
                .HasMany(t => t.Alunos)
                .WithMany(a => a.Turmas)
                .UsingEntity(j => j.ToTable("AlunosTurmas"));

            modelBuilder.Entity<Turma>()
                .HasOne(t => t.Professor)
                .WithMany(p => p.Turmas)
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}