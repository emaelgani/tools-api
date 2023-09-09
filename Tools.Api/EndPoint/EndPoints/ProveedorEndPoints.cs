using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Proveedor;
using Tools.Shared.Exceptions;

namespace Tools.Api.EndPoint.EndPoints
{
    internal static class ProveedorEndPoints
    {
        public static async Task Add(HttpContext context, IProveedorService proveedorSrv, ProveedorDTO proveedorDto)
        {
            try
            {
                if (proveedorDto is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await proveedorSrv.Add(proveedorDto);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
            }

        }

        public static async Task Update(HttpContext context, IProveedorService proveedorSrv, ProveedorDTO proveedorDto)
        {
            try
            {
                if (proveedorDto is null || proveedorDto.IdProveedor is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await proveedorSrv.Update(proveedorDto);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");

            }

        }

        public static async Task AumentarPorcentajeProveedor(HttpContext context, IProveedorService proveedorSrv, ProveedorAumentoDTO proveedorDto)
        {
            try
            {
                if (proveedorDto is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await proveedorSrv.AumentarPorcentajeProveedor(proveedorDto);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");

            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
            }
        }

        public static async Task<IList<ProveedorWithProductsDTO>> GetAllProveedoresAsync(HttpContext context, IProveedorService proveedorSrv)
        {
            try
            {
                return await proveedorSrv.GetAllAsync();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return new List<ProveedorWithProductsDTO>();
            }
        }

        public static async Task<IResult?> CreateListaPreciosPdf(HttpContext context, IProveedorService proveedorSrv, int idProveedor)
        {
            try
            {
                var pdfResult = await proveedorSrv.CreatePdfListaPrecios(idProveedor);

                // Verifica si el resultado es un FileContentResult y si el contenido es un archivo PDF
                if (pdfResult is FileContentResult fileContentResult && fileContentResult.ContentType == "application/pdf")
                {
                    var fileName = fileContentResult.FileDownloadName;
                    return Results.File(fileContentResult.FileContents, contentType: "application/pdf", fileDownloadName: fileName);
                }
                else
                {
                    // Maneja otros tipos de resultados o errores aquí si es necesario
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteAsync("PDF not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return null;
            }
        }
    }
}
