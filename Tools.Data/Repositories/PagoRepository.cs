using Microsoft.EntityFrameworkCore;
using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;

namespace Tools.Data.Repositories
{
    public class PagoRepository : GenericRepository<Pago>, IPagoRepository
    {
        private HerramientasDbContext _context;

        public PagoRepository(HerramientasDbContext db) : base(db)
        {
            _context = db;
        }

        public async Task<decimal> GetLiquidezDigital()
        {
            try
            {
                return  await _context.Pago
                    .Where(p => p.IdMetodoPago == 2)
                    .SumAsync(p => p.TotalPago);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try getting liquidez digital.", ex);
            }
        }

        public async Task<decimal> GetLiquidezEfectivo()
        {
            try
            {
                return await _context.Pago
                    .Where(p => p.IdMetodoPago == 1)
                    .SumAsync(p => p.TotalPago);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try getting liquidez efectivo.", ex);
            }
        }
    }
}
