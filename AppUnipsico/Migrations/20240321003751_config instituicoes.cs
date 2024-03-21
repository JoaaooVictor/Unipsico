using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUnipsico.Migrations
{
    /// <inheritdoc />
    public partial class configinstituicoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estagios_Instituicoes_EstagioId",
                table: "Estagios");

            migrationBuilder.CreateIndex(
                name: "IX_Estagios_InstituicaoId",
                table: "Estagios",
                column: "InstituicaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estagios_Instituicoes_InstituicaoId",
                table: "Estagios",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "InstituicaoId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estagios_Instituicoes_InstituicaoId",
                table: "Estagios");

            migrationBuilder.DropIndex(
                name: "IX_Estagios_InstituicaoId",
                table: "Estagios");

            migrationBuilder.AddForeignKey(
                name: "FK_Estagios_Instituicoes_EstagioId",
                table: "Estagios",
                column: "EstagioId",
                principalTable: "Instituicoes",
                principalColumn: "InstituicaoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
