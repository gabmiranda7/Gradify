using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradify.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarTurmaIdEmAula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tema",
                table: "Aulas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TurmaId",
                table: "Aulas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_TurmaId",
                table: "Aulas",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_Turmas_TurmaId",
                table: "Aulas",
                column: "TurmaId",
                principalTable: "Turmas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); // ✅ Isso evita o erro

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_Turmas_TurmaId",
                table: "Aulas");

            migrationBuilder.DropIndex(
                name: "IX_Aulas_TurmaId",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "Tema",
                table: "Aulas");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "Aulas");
        }
    }
}
