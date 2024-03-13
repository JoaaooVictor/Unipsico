using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUnipsico.Migrations
{
    /// <inheritdoc />
    public partial class configurandoconsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataConsulta",
                table: "Consultas");

            migrationBuilder.AddColumn<Guid>(
                name: "DataConsultaId",
                table: "Consultas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_DataConsultaId",
                table: "Consultas",
                column: "DataConsultaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Datas_DataConsultaId",
                table: "Consultas",
                column: "DataConsultaId",
                principalTable: "Datas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Datas_DataConsultaId",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_DataConsultaId",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "DataConsultaId",
                table: "Consultas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataConsulta",
                table: "Consultas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
