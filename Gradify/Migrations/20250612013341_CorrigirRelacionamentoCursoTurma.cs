using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradify.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirRelacionamentoCursoTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurmasCursos");

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Turmas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CursoId1",
                table: "Turmas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CursoId",
                table: "Turmas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Turmas_CursoId1",
                table: "Turmas",
                column: "CursoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Cursos_CursoId",
                table: "Turmas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turmas_Cursos_CursoId1",
                table: "Turmas",
                column: "CursoId1",
                principalTable: "Cursos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Cursos_CursoId",
                table: "Turmas");

            migrationBuilder.DropForeignKey(
                name: "FK_Turmas_Cursos_CursoId1",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_CursoId",
                table: "Turmas");

            migrationBuilder.DropIndex(
                name: "IX_Turmas_CursoId1",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Turmas");

            migrationBuilder.DropColumn(
                name: "CursoId1",
                table: "Turmas");

            migrationBuilder.CreateTable(
                name: "TurmasCursos",
                columns: table => new
                {
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmasCursos", x => new { x.TurmaId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_TurmasCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmasCursos_Turmas_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turmas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TurmasCursos_CursoId",
                table: "TurmasCursos",
                column: "CursoId");
        }
    }
}
