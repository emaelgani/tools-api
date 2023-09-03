using Microsoft.EntityFrameworkCore;
using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;

namespace Tools.Data.Repositories
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        private readonly HerramientasDbContext _db;

        public PedidoRepository(HerramientasDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IList<Pedido>> GetPedidosWithClientAsync()
        {
            try
            {
                return await _db.Pedido.Include(p => p.Cliente).ToListAsync();
                                
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try getting product with proveedor.", ex);
            }
        }
    }
}
