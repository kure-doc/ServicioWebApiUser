using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioWebApiUser.Migrations
{
    /// <inheritdoc />
    public partial class DatosPedidoVenta_V11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePedidos");

            migrationBuilder.RenameColumn(
                name: "fechaventa",
                table: "OrdenPedidos",
                newName: "FechaPedido");

            migrationBuilder.RenameColumn(
                name: "CondicionesPago",
                table: "OrdenPedidos",
                newName: "MetodoPago");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "OrdenPedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdProducto",
                table: "OrdenPedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Precio",
                table: "OrdenPedidos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "OrdenPedidos");

            migrationBuilder.DropColumn(
                name: "IdProducto",
                table: "OrdenPedidos");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "OrdenPedidos");

            migrationBuilder.RenameColumn(
                name: "MetodoPago",
                table: "OrdenPedidos",
                newName: "CondicionesPago");

            migrationBuilder.RenameColumn(
                name: "FechaPedido",
                table: "OrdenPedidos",
                newName: "fechaventa");

            migrationBuilder.CreateTable(
                name: "DetallePedidos",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedidos", x => x.IdDetalle);
                });
        }
    }
}
