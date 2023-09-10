using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Cliente;
using Tools.Shared.Exceptions;

namespace Tools.Api.EndPoint
{
    internal static class ClienteEndPoints
    {
        public static async Task<IList<ClienteDTO>> GetAllClientesAsync(HttpContext context, IClienteService clienteSrv)
        {
            try
            {
                return await clienteSrv.GetAllAsync();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<ClienteDTO>();
            }
        }

        public static async Task<IList<ClienteVentaProductoDTO>> GetClientesVentaByProducto(HttpContext context, IClienteService clienteSrv, int idProducto)
        {
            try
            {
                return await clienteSrv.GetClientByIdProduct(idProducto);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<ClienteVentaProductoDTO>();
            }
        }

        public static async Task<IList<ClienteDTO>> GetFourMoreDebt(HttpContext context, IClienteService clienteSrv)
        {
            try
            {
                return await clienteSrv.GetFourMoreDebt();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return new List<ClienteDTO>();
            }
        }

        public static async Task<decimal> GetTotalDebt(HttpContext context, IClienteService clienteSrv)
        {
            try
            {
                return await clienteSrv.GetTotalDebt();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return 0;
            }
        }


        public static async Task Add(HttpContext context, IClienteService clienteSrv, ClienteDTO clienteDto)
        {
            try
            {
                if (clienteDto is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await clienteSrv.Add(clienteDto);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");

            }

        }

        public static async Task Update(HttpContext context, IClienteService clienteSrv, ClienteDTO clienteDto)
        {
            try
            {
                if (clienteDto is null || clienteDto.IdCliente is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await clienteSrv.Update(clienteDto);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");

            }

        }

        public static async Task<IResult?> CreatePdfSalida(HttpContext context, IClienteService clienteSrv)
        {
            try
            {
                var pdfResult = await clienteSrv.CreatePdfSalida();

                // Verifica si el resultado es un FileContentResult y si el contenido es un archivo PDF
                if (pdfResult is FileContentResult fileContentResult && fileContentResult.ContentType == "application/pdf")
                {
                    var fileName = fileContentResult.FileDownloadName;
                    return Results.File(fileContentResult.FileContents, contentType: "application/pdf", fileDownloadName: fileName);
                }
                else
                {
                    // Manejar otros tipos de resultados 
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteAsync("PDF not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return null;
            }
        }

    }
}

