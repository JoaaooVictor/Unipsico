using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUnipsico.Migrations
{
    /// <inheritdoc />
    public partial class configurandoconsultas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ConsultaId",
                table: "Consultas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Consultas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ConsultaId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_AspNetUsers_ConsultaId",
                table: "Consultas",
                column: "ConsultaId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_AspNetUsers_ConsultaId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "ConsultaId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ConsultaId",
                table: "Consultas",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
