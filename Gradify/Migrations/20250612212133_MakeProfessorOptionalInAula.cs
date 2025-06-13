using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradify.Migrations
{
    /// <inheritdoc />
    public partial class MakeProfessorOptionalInAula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "Aulas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_ProfessorId",
                table: "Aulas",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Professores_ProfessorId",
                table: "Aulas",
                column: "ProfessorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Professores_ProfessorId",
                table: "Aulas");

            migrationBuilder.DropIndex(
                name: "IX_Aulas_ProfessorId",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Aulas");
        }
    }
}
