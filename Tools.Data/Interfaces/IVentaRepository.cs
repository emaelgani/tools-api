using Microsoft.EntityFrameworkCore.Storage;
using Tools.Data.Entities;
using Tools.Shared.DTOs.Pago;
using Tools.Shared.DTOs.Producto;
using Tools.Shared.DTOs.Venta;

namespace Tools.Data.Interfaces
{
    public interface IVentaRepository : IGenericRepository<Venta>
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();
        Task<IList<VentaDTO>> GetVentasWithClients();
        Task<IList<VentaPorMesDTO>> GetVentasPorMes();
        Task<IList<VentaProductoDTO>> GetVentaProductos(int idVenta);
        Task<IList<VentaPorMesDTO>> GetVentasPorMesByIdProducto(int idProducto);
        Task<IList<ProductoCompradoPorClienteDTO>> GetQuinceProductosMasCompradosPorClientes(string fechaInicio, string fechaFin);
        Task<CobranzasYVentasDTO> GetCobranzaYVenta(string fechaInicio, string fechaFin);
    }
}
