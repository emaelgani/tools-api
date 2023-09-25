using System.Net;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Pago;
using Tools.Shared.DTOs.Producto;
using Tools.Shared.DTOs.Venta;
using Tools.Shared.Exceptions;

namespace Tools.Api.EndPoint.EndPoints
{
    internal static class VentaEndPoints
    {
        public static async Task Register(HttpContext context, IVentaService ventaSrv, VentaCompletaDTO ventaCompletaDto)
        {
            try
            {
                if (ventaCompletaDto is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await ventaSrv.CreateVenta(ventaCompletaDto);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        }

        public static async Task<IList<VentaDTO>> GetVentas(HttpContext context, IVentaService ventaSrv)
        {
            try
            {
                return await ventaSrv.GetVentas();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<VentaDTO>();
            }
        }

        public static async Task<IList<VentaPorMesDTO>> GetVentasPorMes(HttpContext context, IVentaService ventaSrv)
        {
            try
            {
                return await ventaSrv.GetVentasPorMes();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<VentaPorMesDTO>();
            }
        }

        public static async Task<IList<VentaProductoDTO>> GetVentaProductos(HttpContext context, IVentaService ventaSrv, int idVenta)
        {
            try
            {
                return await ventaSrv.GetVentaProductos(idVenta);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<VentaProductoDTO>();
            }
        }

        public static async Task<IList<VentaPorMesDTO>> GetVentasPorMesByIdProducto(HttpContext context, IVentaService ventaSrv, int idProducto)
        {
            try
            {
                return await ventaSrv.GetVentasPorMesByIdProducto(idProducto);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<VentaPorMesDTO>();
            }
        }

        public static async Task<IList<ProductoCompradoPorClienteDTO>> GetQuinceProductosMasCompradosPorClientes(HttpContext context, IVentaService ventaSrv, string fechaInicio, string fechaFin)
        {
            try
            {
                return await ventaSrv.GetQuinceProductosMasCompradosPorClientes(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<ProductoCompradoPorClienteDTO>();
            }
        }

        public static async Task<CobranzasYVentasDTO?> GetCobranzaYVenta(HttpContext context, IVentaService ventaSrv, string fechaInicio, string fechaFin)
        {
            try
            {
                return await ventaSrv.GetCobranzaYVenta(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public static async Task DeleteVenta(HttpContext context, IVentaService ventaService, int id)
        {
            try
            {
                await ventaService.DeleteVenta(id);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        }
    }
}
