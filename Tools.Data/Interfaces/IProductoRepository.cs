using Tools.Data.Entities;
using Tools.Shared.DTOs.Producto;

namespace Tools.Data.Interfaces
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        Task<Producto> GetProductoWithProveedorAsync(int id);

        Task<decimal> CalculateListStockValueAsync();

        Task<decimal> CalculateCashStockValueAsync();

        Task<decimal> CalculateFinanceStockValueAsync();

        Task<IList<Producto2DTO>> GetProductsAsync();
    }
}
