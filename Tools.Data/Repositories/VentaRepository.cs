using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Shared.DTOs.Venta;

namespace Tools.Data.Repositories
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {

        private readonly HerramientasDbContext _context;
        private IDbContextTransaction? _transaction = null;

        public VentaRepository(HerramientasDbContext db) : base(db)
        {
            _context = db;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public void CommitTransaction()
        {
            // Confirmar la transacción
            _transaction?.Commit();
        }

        public async Task<IList<VentaDTO>> GetVentasWithClients()
        {
            try
            {
                var ventas = await _context.Venta
                    .Include(v => v.Cliente)
                    .Select(v => new VentaDTO
                    {
                        IdVenta = v.IdVenta,
                        IdCliente = v.IdCliente,
                        Fecha = v.Fecha,
                        Cliente = v.Cliente!.Nombre != null ? v.Cliente.Nombre : string.Empty,
                        TotalVenta = v.TotalVenta
                    })
                    .ToListAsync();

                return ventas;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred gettins sells", ex);
            }
        }

        public void RollbackTransaction()
        {
            // Revertir la transacción si es necesario
            _transaction?.Rollback();
        }
    }
}
