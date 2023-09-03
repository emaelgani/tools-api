using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;

namespace Tools.Data.Repositories
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedorRepository
    {
        public ProveedorRepository(HerramientasDbContext db) : base(db)
        {
        }
    }
}
