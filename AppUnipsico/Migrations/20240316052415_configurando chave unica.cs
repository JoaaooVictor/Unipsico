using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppUnipsico.Migrations
{
    /// <inheritdoc />
    public partial class configurandochaveunica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RA",
                table: "AspNetUsers",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RA_TipoUsuario",
                table: "AspNetUsers",
                columns: new[] { "RA", "TipoUsuario" },
                unique: true,
                filter: "TipoUsuario = 2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RA_TipoUsuario",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "RA",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);
        }
    }
}
