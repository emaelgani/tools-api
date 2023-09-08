using System.Net;
using Tools.Service.DTOs.Cliente;
using Tools.Service.DTOs.Pago;
using Tools.Service.Interfaces;

namespace Tools.Api.EndPoint.EndPoints
{
    internal static class PagoEndPoints
    {
        public static async Task Add(HttpContext context, IPagoService pagoSrv, PagoDTO pagoDto)
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
                await context.Response.WriteAsync($"An error occurred: {ex}");

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
                await context.Response.WriteAsync($"An error occurred: {ex}");
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
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return 0;
            }
        }
    }
}
