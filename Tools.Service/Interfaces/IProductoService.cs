using Microsoft.AspNetCore.Mvc;
using Tools.Service.DTOs.Producto;
using Tools.Shared.DTOs.Producto;

namespace Tools.Service.Interfaces
{
    public interface IProductoService
    {
        Task<IList<Producto2DTO>> GetAllAsync();
        Task Update(ProductoDTO productoDto);
        Task Add(ProductoDTO productoDto);
        Task DeleteProducto(int id);
        Task<Producto2DTO> GetProducto(int id);
        Task<decimal> GetValorStockLista();
        Task<decimal> GetValorStockFinanciado();
        Task<decimal> GetValorStockContado();
        Task<IActionResult> CreateListaPreciosPdf();
    }
}
