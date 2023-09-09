using Microsoft.EntityFrameworkCore.Storage;
using Tools.Data.Entities;
using Tools.Shared.DTOs.Venta;

namespace Tools.Data.Interfaces
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();
        Task<IList<VentaDTO>> GetVentasWithClients();
    }
}
