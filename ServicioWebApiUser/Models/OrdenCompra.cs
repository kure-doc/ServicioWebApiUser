using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioWebApiUser.Models
{
    public class OrdenCompra
    {
        [Key]
        public int IdOrdenCompra { get; set; }
        public int IdProducto { get; set; }
        public string? NroCompra { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? NombreProveedor { get; set; }
        public DateTime FechaCompra {  get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public double? TotalCompra { get; set; }
        public string? ConfirmarPedido { get; set; }
    }
}
