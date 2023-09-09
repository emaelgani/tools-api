using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;

namespace Tools.Data.Repositories
{
    public class VentaProductoRepository : GenericRepository<VentaProducto>, IVentaProductoRepository
    {
        public VentaProductoRepository(HerramientasDbContext db) : base(db)
        {
        }
    }
}
