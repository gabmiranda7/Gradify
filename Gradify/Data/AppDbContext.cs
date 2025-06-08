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
        public DbSet<TurmaCurso> TurmasCursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TurmaCurso>()
                .HasKey(tc => new { tc.TurmaId, tc.CursoId });

            modelBuilder.Entity<TurmaCurso>()
                .HasOne(tc => tc.Turma)
                .WithMany(t => t.TurmasCursos)
                .HasForeignKey(tc => tc.TurmaId);

            modelBuilder.Entity<TurmaCurso>()
                .HasOne(tc => tc.Curso)
                .WithMany(c => c.TurmasCursos)
                .HasForeignKey(tc => tc.CursoId);

            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Turma)
                .WithMany(t => t.Alunos)
                .HasForeignKey(a => a.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Professor)
                .WithMany(p => p.Cursos)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Turma>()
                .HasOne(t => t.Professor)
                .WithMany(p => p.Turmas)
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Anotacao>()
                .HasOne(a => a.Curso)
                .WithMany(c => c.Anotacoes)
                .HasForeignKey(a => a.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Anotacao>()
                .HasOne(a => a.Aluno)
                .WithMany(al => al.Anotacoes)
                .HasForeignKey(a => a.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Aula>()
                .HasOne(a => a.Curso)
                .WithMany(c => c.Aulas)
                .HasForeignKey(a => a.CursoId)
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