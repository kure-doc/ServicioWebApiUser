using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicioWebApiUser.Models
{
    public class Cliente
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string? IdCliente { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? NombreCliente { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? DirreccionCliente { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string? TelefonoCliente { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Email {  get; set; }
    }
}
