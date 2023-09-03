using System.Net;
using Tools.Service.DTOs;
using Tools.Service.DTOs.Proveedor;
using Tools.Service.Exceptions;
using Tools.Service.Interfaces;

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
    }
}
