using Microsoft.EntityFrameworkCore;
using Tools.Data.Entities;

namespace Tools.Data.DbContexts
{
    public class HerramientasDbContext: DbContext
    {
        public HerramientasDbContext(DbContextOptions<HerramientasDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Producto { get; set; } // Agrega un DbSet para la entidad Producto
        public DbSet<Proveedor> Proveedor { get; set; } // Agrega un DbSet para la entidad Proveedor
        public DbSet<Pedido> Pedido { get; set; } // Agrega un DbSet para la entidad Pedido
        public DbSet<Pago> Pago { get; set; } // Agrega un DbSet para la entidad Pago
        public DbSet<Venta> Venta { get; set; } // Agrega un DbSet para la entidad Venta
        public DbSet<Cliente> Cliente { get; set; } // Agrega un DbSet para la entidad Cliente
        public DbSet<VentaProducto> VentaProducto { get; set; } // Agrega un DbSet para la entidad VentaProducto


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             
            // mapeos de Tablas -> Entidades en nuestro sistema.
            modelBuilder.AddHerramientasTablesConfiguration();
        }
    }
}
