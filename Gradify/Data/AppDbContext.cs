using Gradify.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Data
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Anotacao> Anotacoes { get; set; }
        public DbSet<Aula> Aulas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Frequencia> Frequencias { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Turma> Turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aula>()
                .HasOne(a => a.Professor)
                .WithMany(p => p.Aulas)
                .HasForeignKey(a => a.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
      


            modelBuilder.Entity<Turma>()
                .HasOne(t => t.Curso)
                .WithMany(c => c.Turmas)
                .HasForeignKey(t => t.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Turma)
                .WithMany(t => t.Alunos)
                .HasForeignKey(a => a.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Anotacao>()
                .HasOne(a => a.Aula)
                .WithMany(aula => aula.Anotacoes)
                .HasForeignKey(a => a.AulaId);

            modelBuilder.Entity<Anotacao>()
                .HasOne(a => a.Aluno)
                .WithMany(al => al.Anotacoes)
                .HasForeignKey(a => a.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Aula>()
                .HasOne(a => a.Turma)
                .WithMany(t => t.Aulas)
                .HasForeignKey(a => a.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Frequencia>()
                .HasOne(f => f.Aluno)
                .WithMany(a => a.Frequencias)
                .HasForeignKey(f => f.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Frequencia>()
                .HasOne(f => f.Turma)
                .WithMany()
                .HasForeignKey(f => f.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Aluno)
                .WithOne(a => a.Usuario)
                .HasForeignKey<Aluno>(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Professor)
                .WithOne(p => p.Usuario)
                .HasForeignKey<Professor>(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}