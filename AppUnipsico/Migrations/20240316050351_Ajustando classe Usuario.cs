using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUnipsico.Migrations
{
    /// <inheritdoc />
    public partial class AjustandoclasseUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RA",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RA",
                table: "AspNetUsers");
        }
    }
}
