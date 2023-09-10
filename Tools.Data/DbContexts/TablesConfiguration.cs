using Microsoft.EntityFrameworkCore;
using Tools.Data.Entities;

namespace Tools.Data.DbContexts
{
    public static class TablesConfiguration
    {
        /// <summary>
        /// Se agregan las tablas que seran mapeas en nuestro sistema.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddHerramientasTablesConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(m =>
            {
                m.ToTable("cliente");
                m.HasKey("IdCliente");
                m.Property(c => c.IdCliente).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>(m =>
            {
                m.ToTable("user");
                m.HasKey("IdUser");
                m.Property(c => c.IdUser).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Proveedor>(m =>
            {
                m.ToTable("proveedor");
                m.HasKey("IdProveedor");
                m.Property(c => c.IdProveedor).ValueGeneratedOnAdd();
            });


            modelBuilder.Entity<MetodoPago>(m =>
            {
                m.ToTable("metodopago");
                m.HasKey("IdMetodoPago");
                m.Property(c => c.IdMetodoPago).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TipoPrecio>(m =>
            {
                m.ToTable("tipoprecio");
                m.HasKey("IdTipoPrecio");
                m.Property(c => c.IdTipoPrecio).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Pago>(m =>
            {
                m.ToTable("pago");
                m.HasKey("IdPago");
                m.Property(c => c.IdPago).ValueGeneratedOnAdd();

                // Relación con Cliente
                m.HasOne(p => p.Cliente)
                    .WithMany()
                    .HasForeignKey(p => p.IdCliente)
                    .IsRequired();

                // Relación con MetodoPago
                m.HasOne(p => p.MetodoPago)
                    .WithMany()
                    .HasForeignKey(p => p.IdMetodoPago)
                    .IsRequired();

            });

            modelBuilder.Entity<Producto>(m =>
            {
                m.ToTable("producto");
                m.HasKey("IdProducto");
                m.Property(c => c.IdProducto).ValueGeneratedOnAdd();

                // Configurar la relación con Proveedor
                m.HasOne(p => p.Proveedor)  // Propiedad de navegación en Producto
                    .WithMany(pr => pr.Productos)  // Propiedad de navegación en Proveedor (colección de productos)
                    .HasForeignKey(p => p.IdProveedor);  // Clave foránea en Producto
            });

            modelBuilder.Entity<Compromiso>(m =>
            {
                m.ToTable("compromiso");
                m.HasKey("IdCompromiso");
                m.Property(c => c.IdCompromiso).ValueGeneratedOnAdd();
               
            });

            modelBuilder.Entity<Compromiso>()
               .HasOne(c => c.Proveedor) // Propiedad de navegación en Compromiso
               .WithMany() // No necesitas una propiedad de navegación en Proveedor
               .HasForeignKey(c => c.IdProveedor); // Clave foránea en Compromiso

            modelBuilder.Entity<Pedido>(m =>
            {
                m.ToTable("pedido");
                m.HasKey("IdPedido");
                m.Property(c => c.IdPedido).ValueGeneratedOnAdd();

                // Configurar la relación con Cliente
                m.HasOne(p => p.Cliente)  // Propiedad de navegación en Pedido
                    .WithMany(c => c.Pedidos)  // Propiedad de navegación en Cliente (colección de pedidos)
                    .HasForeignKey(p => p.IdCliente);  // Clave foránea en Pedido
            });

            modelBuilder.Entity<Venta>(m =>
            {
                m.ToTable("venta");
                m.HasKey("IdVenta");
                m.Property(c => c.IdVenta).ValueGeneratedOnAdd();

                // Configurar la relación con Cliente
                m.HasOne(v => v.Cliente)  // Propiedad de navegación en Venta
                    .WithMany(c => c.Ventas)  // Propiedad de navegación en Cliente (colección de ventas)
                    .HasForeignKey(v => v.IdCliente)  // Clave foránea en Venta
                    .IsRequired(); 
            });

            modelBuilder.Entity<VentaProducto>(m =>
            {
                m.ToTable("ventaproducto");
                m.HasKey("IdVentaProducto");
                m.Property(c => c.IdVenta).ValueGeneratedOnAdd();


                // Configurar la relación con Venta
                m.HasOne(vp => vp.Venta)  // Propiedad de navegación en VentaProducto
                    .WithMany(v => v.VentaProductos)  // Propiedad de navegación en Venta (colección de VentaProductos)
                    .HasForeignKey(vp => vp.IdVenta)  // Clave foránea en VentaProducto
                    .IsRequired();

                // Configurar la relación con Producto
                m.HasOne(vp => vp.Producto)  // Propiedad de navegación en VentaProducto
                    .WithMany(p => p.VentaProductos)  // Propiedad de navegación en Producto (colección de VentaProductos)
                    .HasForeignKey(vp => vp.IdProducto)  // Clave foránea en VentaProducto
                    .IsRequired();

                // Configurar la relación con TipoPrecio
                m.HasOne(vp => vp.TipoPrecio)  // Propiedad de navegación en VentaProducto
                    .WithMany(tp => tp.VentaProductos)  // Propiedad de navegación en TipoPrecio (colección de VentaProductos)
                    .HasForeignKey(vp => vp.IdTipoPrecio)  // Clave foránea en VentaProducto
                    .IsRequired();

            });
        }
    }
}
