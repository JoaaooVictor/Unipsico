using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUnipsico.Migrations
{
    /// <inheritdoc />
    public partial class ajusteestagiosdataestagio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFimEstagio",
                table: "Estagios");

            migrationBuilder.RenameColumn(
                name: "DataInicioEstagio",
                table: "Estagios",
                newName: "DataEstagio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataEstagio",
                table: "Estagios",
                newName: "DataInicioEstagio");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFimEstagio",
                table: "Estagios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
