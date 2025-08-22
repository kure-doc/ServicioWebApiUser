using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioWebApiUser.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? NombreProducto { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? CodigoProducto { get; set; }
        public double? PrecioProducto { get; set; }
        public int Cantidad {  get; set; }
    }
}
