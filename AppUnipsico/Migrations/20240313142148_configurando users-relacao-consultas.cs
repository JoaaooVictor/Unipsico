using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUnipsico.Migrations
{
    /// <inheritdoc />
    public partial class configurandousersrelacaoconsultas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_AspNetUsers_ConsultaId",
                table: "Consultas");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Consultas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_UsuarioId",
                table: "Consultas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_AspNetUsers_UsuarioId",
                table: "Consultas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_AspNetUsers_UsuarioId",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_UsuarioId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Consultas");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_AspNetUsers_ConsultaId",
                table: "Consultas",
                column: "ConsultaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
