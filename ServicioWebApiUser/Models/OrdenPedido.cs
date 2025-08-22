using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioWebApiUser.Models
{
    public class OrdenPedido
    {
        [Key]
        public int IdPedido { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? IdCliente { get; set; }
        public int IdProducto { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? FechaPedido { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? MetodoPago { get; set; }
        public int Cantidad { get; set; }
        public double Precio {  get; set; }
        public double TotalDescuento { get; set; }
        public string? ConfirmarPedido { get; set; }
    }
}
