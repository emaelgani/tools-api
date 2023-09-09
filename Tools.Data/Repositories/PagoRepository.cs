using Microsoft.EntityFrameworkCore;
using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Shared.DTOs.Pago;

namespace Tools.Data.Repositories
{
    public class PagoRepository : GenericRepository<Pago>, IPagoRepository
    {
        private HerramientasDbContext _context;

        public PagoRepository(HerramientasDbContext db) : base(db)
        {
            _context = db;
        }

        public async Task<decimal> GetLiquidezDigital()
        {
            try
            {
                return  await _context.Pago
                    .Where(p => p.IdMetodoPago == 2)
                    .SumAsync(p => p.TotalPago);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try getting liquidez digital.", ex);
            }
        }

        public async Task<decimal> GetLiquidezEfectivo()
        {
            try
            {
                return await _context.Pago
                    .Where(p => p.IdMetodoPago == 1)
                    .SumAsync(p => p.TotalPago);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when try getting liquidez efectivo.", ex);
            }
        }

        public new async Task<IList<PagoListDTO>> GetAllAsync()
        {
            try
            {
                var pagos = await _context.Pago
                .Include(p => p.Cliente) // Incluir la entidad Cliente en la consulta
                .Include(p => p.MetodoPago) // Incluir la entidad MetodoPago en la consulta
                .Select(p => new PagoListDTO
                {
                    IdPago = p.IdPago,
                    IdCliente = p.IdCliente,
                    IdMetodoPago = p.IdMetodoPago,
                    Fecha = p.Fecha,
                    TotalPago = p.TotalPago,
                    Cliente = p.Cliente!.Nombre != null ? p.Cliente.Nombre : string.Empty,
                    MetodoPago = p.MetodoPago!.TipoMetodoPago != null ? p.MetodoPago.TipoMetodoPago : string.Empty,
                })
                .ToListAsync();

                return pagos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pagos.", ex);
            }
        }

        public async Task<IList<PagoPorMesDTO>> GetPagosPorMes()
        {
            try
            {
                int anioActual = DateTime.Now.Year;

                var pagosPorMes = await _context.Pago
                    .Where(v => v.Fecha.Year == anioActual)
                    .GroupBy(v => new { v.Fecha.Year, v.Fecha.Month })
                    .Select(g => new PagoPorMesDTO
                    {
                        Mes = g.Key.Month,
                        Anio = g.Key.Year,
                        TotalPagos = g.Sum(v => v.TotalPago)
                    })
                    .ToListAsync();

                return pagosPorMes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pagos y ventas por mes", ex);
            }
        }

        public async Task<PagosYVentasPorMesDTO> GetPagosYVentasPorMes()
        {
            try
            {
                var result = new PagosYVentasPorMesDTO();

                int anioActual = DateTime.Now.Year;

                var pagosPorMes = await _context.Pago
                    .Where(v => v.Fecha.Year == anioActual)
                    .GroupBy(v => new { v.Fecha.Year, v.Fecha.Month })
                    .Select(g => new PagoPorMesDTO
                    {
                        Mes = g.Key.Month,
                        Anio = g.Key.Year,
                        TotalPagos = g.Sum(v => v.TotalPago)
                    })
                    .ToListAsync();

                var ventasPorMes = await _context.Venta
                    .Where(v => v.Fecha.Year == anioActual)
                    .GroupBy(v => new { v.Fecha.Year, v.Fecha.Month })
                    .Select(g => new VentaPorMesDTO
                    {
                        Mes = g.Key.Month,
                        Anio = g.Key.Year,
                        TotalVentas = g.Sum(v => v.TotalVenta)
                    })
                    .ToListAsync();

                result.Ventas = ventasPorMes;
                result.Pagos = pagosPorMes;

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los pagos y ventas por mes", ex);
            }
        }
    }
}
