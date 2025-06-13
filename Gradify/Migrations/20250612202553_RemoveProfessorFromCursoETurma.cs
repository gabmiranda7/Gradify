using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradify.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProfessorFromCursoETurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Professores_ProfessorId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Professores_ProfessorId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_ProfessorId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_ProfessorId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Cursos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Turmas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Cursos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_ProfessorId",
                table: "Turmas",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_ProfessorId",
                table: "Cursos",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Professores_ProfessorId",
                table: "Cursos",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Professores_ProfessorId",
                table: "Turmas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
