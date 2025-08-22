using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioWebApiUser.Migrations
{
    /// <inheritdoc />
    public partial class awa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdCliente",
                table: "Perfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Perfiles");
        }
    }
}
