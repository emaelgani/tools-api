using Tools.Shared.DTOs.Pago;
using Tools.Shared.DTOs.Producto;
using Tools.Shared.DTOs.Venta;

namespace Tools.Service.Interfaces
{
    public interface IVentaService
    {
        Task<IList<VentaDTO>> GetVentas();
        Task<IList<VentaPorMesDTO>> GetVentasPorMes();
        Task<IList<VentaPorMesDTO>> GetVentasPorMesByIdProducto(int idProducto);
        Task<IList<VentaProductoDTO>> GetVentaProductos(int idVenta);
        Task<IList<ProductosPorVentaDTO>> GetProductosPorVentas(string fechaInicio, string fechaFin);
        Task<IList<ProductoCompradoPorClienteDTO>> GetQuinceProductosMasCompradosPorClientes(string fechaInicio, string fechaFin);
        Task<CobranzasYVentasDTO> GetCobranzaYVenta(string fechaInicio, string fechaFin);
        Task CreateVenta(VentaCompletaDTO ventaCompleta);
    }
}
