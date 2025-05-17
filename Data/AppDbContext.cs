using System.Collections.Generic;
using Gradify.Models;
using Microsoft.EntityFrameworkCore;

namespace Gradify.Data
{
    public class AppDbContext : DbContext
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

            modelBuilder.Entity<Usuario>()
                .HasDiscriminator<string>("TipoUsuario")
                .HasValue<Aluno>("Aluno")
                .HasValue<Professor>("Professor");

            modelBuilder.Entity<AlunoTurma>()
                .HasKey(at => new { at.AlunoId, at.TurmaId });

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Aluno)
                .WithMany(a => a.AlunosTurmas)
                .HasForeignKey(at => at.AlunoId);

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Turma)
                .WithMany(t => t.AlunosTurmas)
                .HasForeignKey(at => at.TurmaId);

            modelBuilder.Entity<Anotacao>()
                .HasOne(a => a.Aluno)
                .WithMany(al => al.Anotacoes)
                .HasForeignKey(a => a.AlunoId);

            modelBuilder.Entity<Anotacao>()
                .HasOne(a => a.Turma)
                .WithMany(t => t.Anotacoes)
                .HasForeignKey(a => a.TurmaId);

            modelBuilder.Entity<Aula>()
                .HasOne(a => a.Turma)
                .WithMany(t => t.Aulas)
                .HasForeignKey(a => a.TurmaId);

            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Professor)
                .WithMany(p => p.Cursos)
                .HasForeignKey(c => c.ProfessorId);

            modelBuilder.Entity<Frequencia>()
                .HasOne(f => f.Aluno)
                .WithMany(al => al.Frequencias)
                .HasForeignKey(f => f.AlunoId);

            modelBuilder.Entity<Frequencia>()
                .HasOne(f => f.Turma)
                .WithMany(t => t.Frequencias)
                .HasForeignKey(f => f.TurmaId);

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
        }
    }
}