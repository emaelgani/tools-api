using System.Net;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Venta;

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
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");

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
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return new List<VentaDTO>();
            }
        }
    }
}
