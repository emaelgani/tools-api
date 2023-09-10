﻿using System.Net;
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
                await context.Response.WriteAsync($"An error occurred: {ex}");
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
                await context.Response.WriteAsync($"An error occurred: {ex}");
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
                await context.Response.WriteAsync($"An error occurred: {ex}");
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
                await context.Response.WriteAsync($"An error occurred: {ex}");
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
                await context.Response.WriteAsync($"An error occurred: {ex}");

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

