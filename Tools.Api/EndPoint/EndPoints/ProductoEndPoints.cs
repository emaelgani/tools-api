using Microsoft.AspNetCore.Mvc;
using System.Net;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Producto;
using Tools.Shared.Exceptions;

namespace Tools.Api.EndPoint.EndPoints
{
    internal static class ProductoEndPoints
    {
        public static async Task Add(HttpContext context, IProductoService productoSrv, ProductoDTO productoDto)
        {
            try
            {
                if (productoDto is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await productoSrv.Add(productoDto);
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

        public static async Task Update(HttpContext context, IProductoService productoSrv, ProductoDTO productoDto)
        {
            try
            {
                if (productoDto is null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                await productoSrv.Update(productoDto);
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

        public static async Task Delete(HttpContext context, IProductoService productoSrv, int id)
        {
            try
            {
                await productoSrv.DeleteProducto(id);
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

        public static async Task<Producto2DTO?> GetProducto(HttpContext context, IProductoService productoSrv, int id)
        {
            try
            {
                return await productoSrv.GetProducto(id);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return null;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return null;
            }
        }
            
        // Call stored procedure.
        public static async Task<IList<Producto2DTO>?> GetProductos(HttpContext context, IProductoService productoSrv)
        {
            try
            {
                return await productoSrv.GetAllAsync();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return null;
            }

        }

        public static async Task<decimal?> GetValorStockLista(HttpContext context, IProductoService productoSrv)
        {
            try
            {
                return await productoSrv.GetValorStockLista();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return null;
            }
        }

        public static async Task<decimal?> GetValorStockFinanciado(HttpContext context, IProductoService productoSrv)
        {
            try
            {
                return await productoSrv.GetValorStockFinanciado();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return null;
            }
        }

        public static async Task<decimal?> GetValorStockContado(HttpContext context, IProductoService productoSrv)
        {
            try
            {
                return await productoSrv.GetValorStockContado();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex}");
                return null;
            }
        }

        public static async Task<IResult?> CreateListaPreciosPdf(HttpContext context, IProductoService productoSrv)
        {
            try
            {
                var pdfResult = await productoSrv.CreateListaPreciosPdf();

                // Verifica si el resultado es un FileContentResult y si el contenido es un archivo PDF
                if (pdfResult is FileContentResult fileContentResult && fileContentResult.ContentType == "application/pdf")
                {
                    var fileName = fileContentResult.FileDownloadName;
                    return Results.File(fileContentResult.FileContents, contentType: "application/pdf", fileDownloadName: fileName);
                }
                else
                {
                    // Maneja otros tipos de resultados o errores
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
