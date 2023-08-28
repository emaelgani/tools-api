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

        }
    }
}
