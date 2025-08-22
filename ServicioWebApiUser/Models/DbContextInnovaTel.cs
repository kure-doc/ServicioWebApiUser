using Microsoft.EntityFrameworkCore;

namespace ServicioWebApiUser.Models
{
    public class DbContextInnovaTel : DbContext
    {
        public DbContextInnovaTel()
        {

        }
        public DbContextInnovaTel(DbContextOptions<DbContextInnovaTel> options)
            : base(options)
        { 
        }

        public virtual DbSet<Cliente> Clientes { get; set;} = null!;
        public virtual DbSet<Producto> Productos { get; set;} = null!;
        public virtual DbSet<OrdenPedido> OrdenPedidos { get; set;} = null!;
        public virtual DbSet<OrdenCompra> OrdenCompras { get; set;} = null!;
        public virtual DbSet<Proveedor> Proveedores { get; set;} = null!;
        public virtual DbSet<Perfil> Perfiles { get; set;} = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=UserDBINOOVATEL_2;Integrated Security=SSPI;TrustServerCertificate=True; User ID=sa;Password=********;");
    }
}
