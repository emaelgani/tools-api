﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.Interfaces;
using Tools.Shared.DTOs.Pago;
using Tools.Shared.DTOs.Producto;
using Tools.Shared.DTOs.Venta;
using Tools.Shared.Exceptions;

namespace Tools.Service.Services
{
    public class VentaService : IVentaService
    {
        private readonly IMapper _mapper;
        private readonly IVentaRepository _ventaRepo;
        private readonly IClienteRepository _clienteRepo;
        private readonly IProductoRepository _productoRepo;
        private readonly IVentaProductoRepository _ventaProducRepo;

        public VentaService(IMapper mapper, IVentaRepository ventaRepo, IClienteRepository clienteRepo, IProductoRepository productoRepo, IVentaProductoRepository ventaProducRepo)
        {
            _mapper = mapper;
            _ventaRepo = ventaRepo;
            _clienteRepo = clienteRepo;
            _productoRepo = productoRepo;
            _ventaProducRepo = ventaProducRepo;
        }

        
        public async Task CreateVenta(VentaCompletaDTO ventaCompleta)
        {
            using (var transaction = await _ventaRepo.BeginTransactionAsync())
            {
                try
                {
                    #region asigno venta
                    var venta = _mapper.Map<Venta>(ventaCompleta.Venta);
                    venta.Fecha = venta.Fecha.Date;
                    _ventaRepo.Add(venta);
                    await _ventaRepo.CommitAsync();
                    #endregion

                    #region asigno deuda a cliente
                    var dbCliente = await _clienteRepo.FindByIdAsync(ventaCompleta.Venta.IdCliente);
                    if (dbCliente is null)
                    {
                        throw new NotFoundException("Cliente in venta not found.");
                    }

                    dbCliente.Deuda += ventaCompleta.Venta.TotalVenta;

                    _clienteRepo.Update(dbCliente);
                    await _clienteRepo.CommitAsync();
                    #endregion

                    #region recorro los productos y asigno el id de la venta a cada uno
                    foreach (var ventaProductoDto in ventaCompleta.Productos)
                    {
                        var dbProducto = await _productoRepo.FindByIdAsync(ventaProductoDto.IdProducto);

                        if (dbProducto is null)
                        {
                            throw new NotFoundException("El producto es inexistente.");
                        }
                        if (dbProducto.Stock - ventaProductoDto.Cantidad < 0)
                        {
                            throw new NotFoundException($"Está intentando vender más cantidad de la que hay en stock del producto: '{dbProducto.Nombre}'.");


                        }
                        dbProducto.Stock -=  ventaProductoDto.Cantidad;
                        _productoRepo.Update(dbProducto);
                        await _productoRepo.CommitAsync();

                        var ventaProducto = _mapper.Map<VentaProducto>(ventaProductoDto);
                        ventaProducto.IdVenta = venta.IdVenta;
                        _ventaProducRepo.Add(ventaProducto);
                        await _ventaProducRepo.CommitAsync();
                    }
                    #endregion

                    _ventaRepo.CommitTransaction();

                }
                catch (NotFoundException ex)
                {
                    await transaction.RollbackAsync();
                    throw new NotFoundException(ex.Message);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error al crear la venta", ex);
                }
            }
        }

        public async Task DeleteVenta(int idVenta)
        {
            try
            {
                var venta = await _ventaRepo.FindByIdAsync(idVenta);
                var ventaProductos =  await _ventaProducRepo.FindByCondition(vp => vp.IdVenta == idVenta).ToListAsync();
                var cliente = await _clienteRepo.FindByIdAsync(venta!.IdCliente);
                cliente!.Deuda -= venta.TotalVenta;

                foreach (var ventaProducto in ventaProductos)
                {
                    var prod = await _productoRepo.FindByIdAsync(ventaProducto.IdProducto);
                    prod!.Stock += ventaProducto.Cantidad;
                    
                }

                _ventaProducRepo.RemoveRange(ventaProductos);
                await _ventaProducRepo.CommitAsync();
                await _ventaRepo.RemoveByIdAsync(idVenta);
                await _productoRepo.CommitAsync();
                await _ventaRepo.CommitAsync();
                _clienteRepo.Update(cliente);
                await _clienteRepo.CommitAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar la venta.");
            }
        }

        public async Task<CobranzasYVentasDTO> GetCobranzaYVenta(string fechaInicio, string fechaFin)
        {
            try
            {
                return await _ventaRepo.GetCobranzaYVenta(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        //TODO: revisar si eliminar metodo
        public Task<IList<ProductosPorVentaDTO>> GetProductosPorVentas(string fechaInicio, string fechaFin)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<ProductoCompradoPorClienteDTO>> GetQuinceProductosMasCompradosPorClientes(string fechaInicio, string fechaFin)
        {
            try
            {
                return await _ventaRepo.GetQuinceProductosMasCompradosPorClientes(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IList<VentaProductoDTO>> GetVentaProductos(int idVenta)
        {
            try
            {
                return await _ventaRepo.GetVentaProductos(idVenta);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IList<VentaDTO>> GetVentas()
        {
            try
            {
                return await _ventaRepo.GetVentasWithClients();
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IList<VentaPorMesDTO>> GetVentasPorMes()
        {
            try
            {
                return await _ventaRepo.GetVentasPorMes();
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }

        public async Task<IList<VentaPorMesDTO>> GetVentasPorMesByIdProducto(int idProducto)
        {
            try
            {
                return await _ventaRepo.GetVentasPorMesByIdProducto(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
        }
    }
}
