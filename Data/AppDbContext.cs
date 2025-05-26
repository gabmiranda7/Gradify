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
        public DbSet<AlunoTurma> AlunosTurmas { get; set; }
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

            // Chave composta: AlunoTurma
            modelBuilder.Entity<AlunoTurma>()
                .HasKey(at => new { at.AlunoId, at.TurmaId });

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Aluno)
                .WithMany(a => a.AlunosTurmas)
                .HasForeignKey(at => at.AlunoId)
                .OnDelete(DeleteBehavior.Restrict); // Não pode excluir Aluno se tiver Turmas

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Turma)
                .WithMany(t => t.AlunosTurmas)
                .HasForeignKey(at => at.TurmaId)
                .OnDelete(DeleteBehavior.Cascade); // Ok excluir Turma, remove associação

            // Chave composta: TurmaCurso
            modelBuilder.Entity<TurmaCurso>()
                .HasKey(tc => new { tc.TurmaId, tc.CursoId });

            modelBuilder.Entity<TurmaCurso>()
                .HasOne(tc => tc.Turma)
                .WithMany(t => t.TurmasCursos)
                .HasForeignKey(tc => tc.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TurmaCurso>()
                .HasOne(tc => tc.Curso)
                .WithMany(c => c.TurmasCursos)
                .HasForeignKey(tc => tc.CursoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Anotação -> Turma
            modelBuilder.Entity<Anotacao>()
                .HasOne(a => a.Turma)
                .WithMany(t => t.Anotacoes)
                .HasForeignKey(a => a.TurmaId)
                .OnDelete(DeleteBehavior.Cascade); // Ok excluir Turma, remove Anotações

            // Opcional: se tiver Anotação -> Aluno
            modelBuilder.Entity<Anotacao>()
                .HasOne(a => a.Aluno)
                .WithMany(al => al.Anotacoes)
                .HasForeignKey(a => a.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Aula -> Turma
            modelBuilder.Entity<Aula>()
                .HasOne(a => a.Turma)
                .WithMany(t => t.Aulas)
                .HasForeignKey(a => a.TurmaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Curso -> Professor
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Professor)
                .WithMany(p => p.Cursos)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict); // Não pode excluir Professor se tiver Curso

            // Frequência -> Aluno
            modelBuilder.Entity<Frequencia>()
                .HasOne(f => f.Aluno)
                .WithMany(a => a.Frequencias)
                .HasForeignKey(f => f.AlunoId)
                .OnDelete(DeleteBehavior.Restrict); // Não excluir Aluno se tiver Frequência

            // Frequência -> Turma
            modelBuilder.Entity<Frequencia>()
                .HasOne(f => f.Turma)
                .WithMany(t => t.Frequencias)
                .HasForeignKey(f => f.TurmaId)
                .OnDelete(DeleteBehavior.Cascade); // Ok excluir Turma, remove Frequências
        }
    }
}