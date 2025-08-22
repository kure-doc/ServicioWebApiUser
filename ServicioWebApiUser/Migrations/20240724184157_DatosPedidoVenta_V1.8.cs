using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioWebApiUser.Migrations
{
    /// <inheritdoc />
    public partial class DatosPedidoVenta_V18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdenCompras",
                columns: table => new
                {
                    IdOrdenCompra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    NombreProveedor = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    TotalCompra = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenCompras", x => x.IdOrdenCompra);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdenCompras");
        }
    }
}
