using System.Net;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Pedido;
using Tools.Shared.Exceptions;

namespace Tools.Api.EndPoint.EndPoints
{
    internal static class PedidoEndPoints
    {
        public static async Task<IList<PedidoWithClientDTO>> GetAllPedidosAsync(HttpContext context, IPedidoService pedidoSrv)
        {
            try
            {
                return await pedidoSrv.GetPedidos();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return new List<PedidoWithClientDTO>();
            }
        }

        public static async Task Add(HttpContext context, IPedidoService pedidoSrv, PedidoDTO pedidoDto)
        {
            try
            {
                 await pedidoSrv.CreatePedido(pedidoDto);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync($"An error occurred: {ex}");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
            }
        }

        public static async Task Delete(HttpContext context, IPedidoService pedidoSrv, int id)
        {
            try
            {
                await pedidoSrv.DeletePedido(id);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync($"An error occurred: {ex}");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
            }
        }

        public static async Task ChangeStatus(HttpContext context, IPedidoService pedidoSrv, int id)
        {
            try
            {
                await pedidoSrv.ChangeStatus(id);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync($"An error occurred: {ex}");
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
            }
        }
    }
}
