using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServicioWebApiUser.Migrations
{
    /// <inheritdoc />
    public partial class DatosPedidoVenta_V110 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPedido",
                table: "OrdenPedidos",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmarPedido",
                table: "OrdenCompras",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmarPedido",
                table: "OrdenCompras");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPedido",
                table: "OrdenPedidos",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);
        }
    }
}
