﻿// <auto-generated />
using System;
using Gradify.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gradify.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250523001936_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gradify.Models.AlunoTurma", b =>
                {
                    b.Property<int>("AlunoId")
                        .HasColumnType("int");

                    b.Property<int>("TurmaId")
                        .HasColumnType("int");

                    b.HasKey("AlunoId", "TurmaId");

                    b.HasIndex("TurmaId");

                    b.ToTable("AlunosTurmas");
                });

            modelBuilder.Entity("Gradify.Models.Anotacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlunoId")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TurmaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("TurmaId");

                    b.ToTable("Anotacoes");
                });

            modelBuilder.Entity("Gradify.Models.Aula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAula")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("HoraFimAula")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraInicioAula")
                        .HasColumnType("time");

                    b.Property<int>("TurmaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TurmaId");

                    b.ToTable("Aulas");
                });

            modelBuilder.Entity("Gradify.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Gradify.Models.Frequencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlunoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Presente")
                        .HasColumnType("bit");

                    b.Property<int>("TurmaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("TurmaId");

                    b.ToTable("Frequencias");
                });

            modelBuilder.Entity("Gradify.Models.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("Gradify.Models.TurmaCurso", b =>
                {
                    b.Property<int>("TurmaId")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.HasKey("TurmaId", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("TurmasCursos");
                });

            modelBuilder.Entity("Gradify.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoUsuario")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasDiscriminator<string>("TipoUsuario").HasValue("Usuario");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Gradify.Models.Aluno", b =>
                {
                    b.HasBaseType("Gradify.Models.Usuario");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Aluno");
                });

            modelBuilder.Entity("Gradify.Models.Professor", b =>
                {
                    b.HasBaseType("Gradify.Models.Usuario");

                    b.HasDiscriminator().HasValue("Professor");
                });

            modelBuilder.Entity("Gradify.Models.AlunoTurma", b =>
                {
                    b.HasOne("Gradify.Models.Aluno", "Aluno")
                        .WithMany("AlunosTurmas")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gradify.Models.Turma", "Turma")
                        .WithMany("AlunosTurmas")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aluno");

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("Gradify.Models.Anotacao", b =>
                {
                    b.HasOne("Gradify.Models.Aluno", "Aluno")
                        .WithMany("Anotacoes")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gradify.Models.Turma", "Turma")
                        .WithMany("Anotacoes")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aluno");

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("Gradify.Models.Aula", b =>
                {
                    b.HasOne("Gradify.Models.Turma", "Turma")
                        .WithMany("Aulas")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("Gradify.Models.Curso", b =>
                {
                    b.HasOne("Gradify.Models.Professor", "Professor")
                        .WithMany("Cursos")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("Gradify.Models.Frequencia", b =>
                {
                    b.HasOne("Gradify.Models.Aluno", "Aluno")
                        .WithMany("Frequencias")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gradify.Models.Turma", "Turma")
                        .WithMany("Frequencias")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aluno");

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("Gradify.Models.Turma", b =>
                {
                    b.HasOne("Gradify.Models.Professor", "Professor")
                        .WithMany("Turmas")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("Gradify.Models.TurmaCurso", b =>
                {
                    b.HasOne("Gradify.Models.Curso", "Curso")
                        .WithMany("TurmasCursos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradify.Models.Turma", "Turma")
                        .WithMany("TurmasCursos")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("Gradify.Models.Curso", b =>
                {
                    b.Navigation("TurmasCursos");
                });

            modelBuilder.Entity("Gradify.Models.Turma", b =>
                {
                    b.Navigation("AlunosTurmas");

                    b.Navigation("Anotacoes");

                    b.Navigation("Aulas");

                    b.Navigation("Frequencias");

                    b.Navigation("TurmasCursos");
                });

            modelBuilder.Entity("Gradify.Models.Aluno", b =>
                {
                    b.Navigation("AlunosTurmas");

                    b.Navigation("Anotacoes");

                    b.Navigation("Frequencias");
                });

            modelBuilder.Entity("Gradify.Models.Professor", b =>
                {
                    b.Navigation("Cursos");

                    b.Navigation("Turmas");
                });
#pragma warning restore 612, 618
        }
    }
}
