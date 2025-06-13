using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradify.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirRelacionamentoAulaTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Cursos_CursoId",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "HoraFim",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "Aulas");

            migrationBuilder.AlterColumn<int>(
                name: "CursoId",
                table: "Aulas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Cursos_CursoId",
                table: "Aulas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Cursos_CursoId",
                table: "Aulas");

            migrationBuilder.AlterColumn<int>(
                name: "CursoId",
                table: "Aulas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraFim",
                table: "Aulas",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "HoraInicio",
                table: "Aulas",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Cursos_CursoId",
                table: "Aulas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
