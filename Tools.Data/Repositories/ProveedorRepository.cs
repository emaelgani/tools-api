using Microsoft.EntityFrameworkCore;
using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;

namespace Tools.Data.Repositories
{
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedorRepository
    {
        private readonly HerramientasDbContext _db;

        public ProveedorRepository(HerramientasDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Proveedor?> GetProveedorWithProductsAsync(int id)
        {
           return await _db.Proveedor.Include(p => p.Productos).FirstOrDefaultAsync(p => p.IdProveedor == id);
        }

        public async Task<IList<Proveedor>?> GetAllProveedorWithProductsAsync()
        {
            return await _db.Proveedor.Include(p => p.Productos).ToListAsync();
        }
    }
}
