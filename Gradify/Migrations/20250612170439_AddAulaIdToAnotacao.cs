using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gradify.Migrations
{
    /// <inheritdoc />
    public partial class AddAulaIdToAnotacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AulaId",
                table: "Frequencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Presente",
                table: "Frequencias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AulaId",
                table: "Anotacoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Frequencias_AulaId",
                table: "Frequencias",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Anotacoes_AulaId",
                table: "Anotacoes",
                column: "AulaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anotacoes_Aulas_AulaId",
                table: "Anotacoes",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Frequencias_Aulas_AulaId",
                table: "Frequencias",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anotacoes_Aulas_AulaId",
                table: "Anotacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Frequencias_Aulas_AulaId",
                table: "Frequencias");

            migrationBuilder.DropIndex(
                name: "IX_Frequencias_AulaId",
                table: "Frequencias");

            migrationBuilder.DropIndex(
                name: "IX_Anotacoes_AulaId",
                table: "Anotacoes");

            migrationBuilder.DropColumn(
                name: "AulaId",
                table: "Frequencias");

            migrationBuilder.DropColumn(
                name: "Presente",
                table: "Frequencias");

            migrationBuilder.DropColumn(
                name: "AulaId",
                table: "Anotacoes");
        }
    }
}
