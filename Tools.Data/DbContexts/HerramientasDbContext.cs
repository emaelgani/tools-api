using Microsoft.EntityFrameworkCore;

namespace Tools.Data.DbContexts
{
    public class HerramientasDbContext: DbContext
    {
        public HerramientasDbContext(DbContextOptions<HerramientasDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // mapeos de Tablas -> Entidades en nuestro sistema.
            modelBuilder.AddHerramientasTablesConfiguration();
        }
    }
}
