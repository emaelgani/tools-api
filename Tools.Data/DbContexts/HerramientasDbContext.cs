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
        public DbSet<Proveedor> Proveedor { get; set; } // Agrega un DbSet para la entidad Producto
        public DbSet<Pedido> Pedido { get; set; } // Agrega un DbSet para la entidad Producto
        public DbSet<Pago> Pago { get; set; } // Agrega un DbSet para la entidad Producto


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // mapeos de Tablas -> Entidades en nuestro sistema.
            modelBuilder.AddHerramientasTablesConfiguration();
        }
    }
}
