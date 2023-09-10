using System.Net;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Pago;

namespace Tools.Api.EndPoint.EndPoints
{
    internal static class PagoEndPoints
    {

        public static async Task<IList<PagoListDTO>> GetAllPagosAsync(HttpContext context, IPagoService pagoSrv)
        {
            try
            {
                return await pagoSrv.GetAllAsync();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<PagoListDTO>();
            }
        }

        public static async Task<IList<PagoPorMesDTO>> GetPagosPorMes(HttpContext context, IPagoService pagoSrv)
        {
            try
            {
                return await pagoSrv.GetPagosPorMes();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<PagoPorMesDTO>();
            }
        }

        public static async Task<PagosYVentasPorMesDTO> GetPagosYVentasPorMes(HttpContext context, IPagoService pagoSrv)
        {
            try
            {
                return await pagoSrv.GetPagosYVentasPorMes();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new PagosYVentasPorMesDTO();
            }
        }

        public static async Task Add(HttpContext context, IPagoService pagoSrv, PagoAddDTO pagoDto)
        {
            try
            {
                if (pagoDto is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await pagoSrv.Add(pagoDto);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");

            }

        }

        public static async Task<decimal> GetLiquidezDigital(HttpContext context, IPagoService pagoSrv)
        {
            try
            {
                return await pagoSrv.GetLiquidezDigital();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return 0;
            }
        }

        public static async Task<decimal> GetLiquidezEfectivo(HttpContext context, IPagoService pagoSrv)
        {
            try
            {
                return await pagoSrv.GetLiquidezEfectivo();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return 0;
            }
        }
    }
}
