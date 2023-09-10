using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
using System.Data;
using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Shared.DTOs.Pago;
using Tools.Shared.DTOs.Producto;
using Tools.Shared.DTOs.Venta;

namespace Tools.Data.Repositories
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {

        private readonly HerramientasDbContext _context;
        private IDbContextTransaction? _transaction = null;

        public VentaRepository(HerramientasDbContext db) : base(db)
        {
            _context = db;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public void CommitTransaction()
        {
            // Confirmar la transacción
            _transaction?.Commit();
        }

        public async Task<CobranzasYVentasDTO> GetCobranzaYVenta(string fechaInicio, string fechaFin)
        {
            try
            {
                DateTime fechaInicioParam = DateTime.Parse(fechaInicio);
                DateTime fechaFinParam = DateTime.Parse(fechaFin).AddDays(1); // Aseguramos que la fechaFin incluya el último día

                decimal totalCobranzas = await _context.Pago
                    .Where(p => p.Fecha >= fechaInicioParam && p.Fecha < fechaFinParam)
                    .SumAsync(p => p.TotalPago);

                decimal totalVentas = await _context.Venta
                    .Where(v => v.Fecha >= fechaInicioParam && v.Fecha < fechaFinParam)
                    .SumAsync(v => v.TotalVenta);

                var cobranzasYVentas = new CobranzasYVentasDTO
                {
                    TotalCobranzas = totalCobranzas,
                    TotalVentas = totalVentas
                };

                return cobranzasYVentas;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las cobranzas y ventas", ex);
            }
        }

        public async Task<IList<ProductoCompradoPorClienteDTO>> GetQuinceProductosMasCompradosPorClientes(string fechaInicio, string fechaFin)
        {
            var fechaInicioParam = new MySqlParameter("@FechaInicio", fechaInicio);
            var fechaFinParam = new MySqlParameter("@FechaFin", fechaFin);

            var productosMasCompradosPorCliente = new List<ProductoCompradoPorClienteDTO>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "CALL GetQuinceProductosMasCompradosPorClientes(@FechaInicio, @FechaFin)";
                command.Parameters.Add(fechaInicioParam);
                command.Parameters.Add(fechaFinParam);

                if (command.Connection?.State != ConnectionState.Open)
                    await command.Connection!.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var productoMasCompradoPorCliente = new ProductoCompradoPorClienteDTO
                        {
                            NombreCliente = reader.GetString(0),
                            NombreProducto = reader.GetString(1),
                            CantidadProductos = reader.GetInt32(2),
                        };

                        productosMasCompradosPorCliente.Add(productoMasCompradoPorCliente);
                    }
                }
            }

            return productosMasCompradosPorCliente;
        }


        public async Task<IList<VentaProductoDTO>> GetVentaProductos(int idVenta)
        {
            try
            {
                var ventaProductos = await _context.VentaProducto
                    .Where(vp => vp.IdVenta == idVenta)
                    .Include(vp => vp.Producto)
                    .Include(vp => vp.TipoPrecio)
                    .Select(vp => new VentaProductoDTO
                    {
                        IdVentaProducto = vp.IdVentaProducto,
                        IdVenta = vp.IdVenta,
                        IdProducto = vp.IdProducto,
                        Producto = vp.Producto!.Nombre != null ? vp.Producto.Nombre : string.Empty,
                        Cantidad = vp.Cantidad,
                        PrecioUnidad = vp.PrecioUnidad,
                        Total = vp.Total,
                        Proveedor = vp.Producto!.Proveedor!.Nombre != null ? vp.Producto!.Proveedor!.Nombre : string.Empty,
                        Descripcion = vp.Producto!.Descripcion != null ? vp.Producto!.Descripcion : string.Empty,
                        TipoPrecio = vp.TipoPrecio!.Tipo
                    })
                    .ToListAsync();

                return ventaProductos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos de la venta", ex);
            }
        }

        public async Task<IList<VentaPorMesDTO>> GetVentasPorMes()
        {
            try
            {
                int anio = DateTime.Now.Year;

                var ventasPorMes = await _context.Venta
                    .Where(v => v.Fecha.Year == anio)
                    .GroupBy(v => new { v.Fecha.Year, v.Fecha.Month })
                    .Select(g => new VentaPorMesDTO
                    {
                        Mes = g.Key.Month,
                        Anio = g.Key.Year,
                        TotalVentas = g.Sum(v => v.TotalVenta)
                    })
                    .ToListAsync();

                return ventasPorMes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ventas por mes", ex);
            }
        }

        public async Task<IList<VentaPorMesDTO>> GetVentasPorMesByIdProducto(int idProducto)
        {
            try
            {
                int anio = DateTime.Now.Year;

                var ventasPorMes = await _context.VentaProducto
                    .Where(v => v.IdProducto == idProducto && v.Venta!.Fecha.Year == anio)
                    .Join(_context.Producto,
                        vp => vp.IdProducto,
                        p => p.IdProducto,
                        (vp, p) => new { VentaProducto = vp, Producto = p })
                    .GroupBy(v => new { v.VentaProducto.Venta!.Fecha.Year, v.VentaProducto.Venta!.Fecha.Month })
                    .Select(g => new VentaPorMesDTO
                    {
                        NombreProducto = g.First().Producto.Nombre,
                        Mes = g.Key.Month,
                        Anio = g.Key.Year,
                        TotalVentas = g.Sum(v => v.VentaProducto.Cantidad)
                    })
                    .ToListAsync();

                return ventasPorMes;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las ventas por mes del producto", ex);
            }
        }

        public async Task<IList<VentaDTO>> GetVentasWithClients()
        {
            try
            {
                var ventas = await _context.Venta
                    .Include(v => v.Cliente)
                    .Select(v => new VentaDTO
                    {
                        IdVenta = v.IdVenta,
                        IdCliente = v.IdCliente,
                        Fecha = v.Fecha,
                        Cliente = v.Cliente!.Nombre != null ? v.Cliente.Nombre : string.Empty,
                        TotalVenta = v.TotalVenta
                    })
                    .ToListAsync();

                return ventas;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred gettins sells", ex);
            }
        }

        public void RollbackTransaction()
        {
            // Revertir la transacción si es necesario
            _transaction?.Rollback();
        }
    }
}
