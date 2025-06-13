using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradify.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCursoFromAnotacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anotacoes_Cursos_CursoId",
                table: "Anotacoes");

            migrationBuilder.AlterColumn<int>(
                name: "CursoId",
                table: "Anotacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Anotacoes_Cursos_CursoId",
                table: "Anotacoes",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anotacoes_Cursos_CursoId",
                table: "Anotacoes");

            migrationBuilder.AlterColumn<int>(
                name: "CursoId",
                table: "Anotacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Anotacoes_Cursos_CursoId",
                table: "Anotacoes",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
