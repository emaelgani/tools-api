using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Cliente;
using Tools.Shared.Exceptions;

namespace Tools.Api.EndPoint
{
    internal static class ClienteEndPoints
    {
        public static async Task<IList<ClienteDTO>> GetAllClientesAsync(HttpContext context, IClienteService clienteSrv, IUserService userSrv)
        {
            try
            {
                // Obtener el token desde el header "Authorization"
                var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                // Log para verificar si el header se obtuvo correctamente
                Console.WriteLine("Authorization Header: " + authorizationHeader);

                // Verificar si el token fue enviado y si comienza con "Bearer "
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync("Token not provided or invalid.");

                    // Completar explícitamente la respuesta para asegurar que se envía el código de estado
                    await context.Response.CompleteAsync();

                    return new List<ClienteDTO>();
                }

                // Extraer el token JWT sin el prefijo "Bearer "
                var token = authorizationHeader.Substring("Bearer ".Length);

                // Validar el token usando el servicio de usuario
                var user = await userSrv.CheckToken();

                if (user == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync("Invalid or expired token.");

                    await context.Response.CompleteAsync();

                    return new List<ClienteDTO>();
                }

                // Si el token es válido, proceder con la lógica de negocio
                var clientes = await clienteSrv.GetAllAsync();
                return clientes;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");

                await context.Response.CompleteAsync();

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

