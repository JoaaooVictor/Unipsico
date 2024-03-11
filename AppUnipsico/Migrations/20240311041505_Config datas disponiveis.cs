using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUnipsico.Migrations
{
    /// <inheritdoc />
    public partial class Configdatasdisponiveis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatasDisponiveis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatasDisponiveis", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatasDisponiveis");
        }
    }
}
