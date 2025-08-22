using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioWebApiUser.Migrations
{
    /// <inheritdoc />
    public partial class nrocompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Proveedores");

            migrationBuilder.AddColumn<string>(
                name: "NroCompra",
                table: "OrdenCompras",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NroCompra",
                table: "OrdenCompras");

            migrationBuilder.AddColumn<string>(
                name: "IdCliente",
                table: "Proveedores",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
