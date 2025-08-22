using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioWebApiUser.Migrations
{
    /// <inheritdoc />
    public partial class DatosPedidoVenta_V12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpuestosTotales",
                table: "OrdenPedidos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "OrdenPedidos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ImpuestosTotales",
                table: "OrdenPedidos",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "OrdenPedidos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
