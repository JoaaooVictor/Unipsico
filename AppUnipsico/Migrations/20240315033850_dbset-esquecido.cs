using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUnipsico.Migrations
{
    /// <inheritdoc />
    public partial class dbsetesquecido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estagio_AspNetUsers_AlunoId",
                table: "Estagio");

            migrationBuilder.DropForeignKey(
                name: "FK_Estagio_Instituicao_EstagioId",
                table: "Estagio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instituicao",
                table: "Instituicao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estagio",
                table: "Estagio");

            migrationBuilder.RenameTable(
                name: "Instituicao",
                newName: "Instituicoes");

            migrationBuilder.RenameTable(
                name: "Estagio",
                newName: "Estagios");

            migrationBuilder.RenameIndex(
                name: "IX_Estagio_AlunoId",
                table: "Estagios",
                newName: "IX_Estagios_AlunoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instituicoes",
                table: "Instituicoes",
                column: "InstituicaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estagios",
                table: "Estagios",
                column: "EstagioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estagios_AspNetUsers_AlunoId",
                table: "Estagios",
                column: "AlunoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estagios_Instituicoes_EstagioId",
                table: "Estagios",
                column: "EstagioId",
                principalTable: "Instituicoes",
                principalColumn: "InstituicaoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estagios_AspNetUsers_AlunoId",
                table: "Estagios");

            migrationBuilder.DropForeignKey(
                name: "FK_Estagios_Instituicoes_EstagioId",
                table: "Estagios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instituicoes",
                table: "Instituicoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Estagios",
                table: "Estagios");

            migrationBuilder.RenameTable(
                name: "Instituicoes",
                newName: "Instituicao");

            migrationBuilder.RenameTable(
                name: "Estagios",
                newName: "Estagio");

            migrationBuilder.RenameIndex(
                name: "IX_Estagios_AlunoId",
                table: "Estagio",
                newName: "IX_Estagio_AlunoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instituicao",
                table: "Instituicao",
                column: "InstituicaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Estagio",
                table: "Estagio",
                column: "EstagioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estagio_AspNetUsers_AlunoId",
                table: "Estagio",
                column: "AlunoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Estagio_Instituicao_EstagioId",
                table: "Estagio",
                column: "EstagioId",
                principalTable: "Instituicao",
                principalColumn: "InstituicaoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
