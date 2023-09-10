using System.Net;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Compromiso;

namespace Tools.Api.EndPoint.EndPoints
{
    internal static class CompromisoEndPoints
    {
        public static async Task Add(HttpContext context, ICompromisoService compromisoSrv, CompromisoDTO compromisoDto)
        {
            try
            {
                if (compromisoDto is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await compromisoSrv.CreateCompromiso(compromisoDto);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }

        }

        public static async Task<IList<CompromisoDTO>> GetAllAsync(HttpContext context, ICompromisoService compromisoSrv )
        {
            try
            {

                return await compromisoSrv.GetCompromisos();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<CompromisoDTO>();
            }

        }

        public static async Task<CompromisoDTO> GetByIdCompromiso(HttpContext context, ICompromisoService compromisoSrv, int idCompromiso)
        {
            try
            {
                return await compromisoSrv.GetCompromisoById(idCompromiso);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new CompromisoDTO();
            }
        }

        public static async Task Update(HttpContext context, ICompromisoService compromisoSrv, CompromisoDTO compromisoDto)
        {
            try
            {
                 await compromisoSrv.UpdateCompromiso(compromisoDto);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        }

        public static async Task Delete(HttpContext context, ICompromisoService compromisoSrv, int idCompromiso)
        {
            try
            {
                await compromisoSrv.DeleteCompromiso(idCompromiso);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
            }
        }

        public static async Task<decimal?> GetMontoCompromisosNoPagados(HttpContext context, ICompromisoService compromisoSrv)
        {
            try
            {
                return await compromisoSrv.GetMontoTotal();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public static async Task<decimal?> GetTotalEfectivo(HttpContext context, ICompromisoService compromisoSrv)
        {
            try
            {
                return await compromisoSrv.GetTotalEfectivo();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public static async Task<decimal?> GetTotalDigital(HttpContext context, ICompromisoService compromisoSrv)
        {
            try
            {
                return await compromisoSrv.GetTotalDigital();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public static async Task<decimal> GetCompromisosNoSaldadosDelDia(HttpContext context, ICompromisoService compromisoSrv)
        {
            try
            {
                return await compromisoSrv.GetCompromisosNoSaldadosDelDia();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return 0;
            }
        }

        public static async Task<decimal> GetGastos(HttpContext context, ICompromisoService compromisoSrv, string fechaInicio, string fechaFin)
        {
            try
            {
                return await compromisoSrv.GetGastos(fechaInicio, fechaFin);
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
