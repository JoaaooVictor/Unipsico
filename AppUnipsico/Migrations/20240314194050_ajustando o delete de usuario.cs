using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUnipsico.Migrations
{
    /// <inheritdoc />
    public partial class ajustandoodeletedeusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_AspNetUsers_UsuarioId",
                table: "Consultas");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_AspNetUsers_UsuarioId",
                table: "Consultas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_AspNetUsers_UsuarioId",
                table: "Consultas");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_AspNetUsers_UsuarioId",
                table: "Consultas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
