using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradify.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirCursoIdFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CursoId",
                table: "Turmas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CursoId",
                table: "Turmas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CursoId1",
                table: "Turmas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CursoId1",
                table: "Turmas",
                column: "CursoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Cursos_CursoId1",
                table: "Turmas",
                column: "CursoId1",
                principalTable: "Cursos",
                principalColumn: "Id");
        }
    }
}
