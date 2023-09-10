using Microsoft.EntityFrameworkCore;
using Tools.Data.DbContexts;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Shared.DTOs.Compromiso;
using Tools.Shared.Exceptions;

namespace Tools.Data.Repositories
{
    public class CompromisoRepository : GenericRepository<Compromiso>, ICompromisoRepository
    {
        private readonly HerramientasDbContext _context;

        public CompromisoRepository(HerramientasDbContext db) : base(db)
        {
            _context = db;
        }

        public async Task CreateCompromiso(Compromiso newCompromiso)
        {
            try
            {
                #region verifico la existencia del proveedor
                var dbProveedor = await _context.Proveedor.FindAsync(newCompromiso.IdProveedor);

                if (dbProveedor == null)
                {
                    throw new NotFoundException("Proveedor not found.");
                }
                #endregion

                
                _context.Compromiso.Add(newCompromiso);

                await _context.SaveChangesAsync();
              
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el compromiso", ex);
            }
        }

        public async Task<Compromiso?> GetCompromisoByIdWithProveedor(int idCompromiso)
        {
            try
            {
                return await _context.Compromiso
                                .Include(p => p.Proveedor)
                                .SingleOrDefaultAsync(p => p.IdCompromiso == idCompromiso);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener compromiso by id with proveedor", ex);
            }
        }

        public async Task<IList<Compromiso>> GetCompromisos()
        {
            try
            {
                return  await _context.Compromiso
                    .Include(p => p.Proveedor)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener compromisos", ex);
            }
        }

        public async Task<decimal> GetGastos(string fechaInicio, string fechaFin)
        {
            try
            {
                _ = DateTime.TryParse(fechaInicio, out var start);
                _ = DateTime.TryParse(fechaFin, out var end);

                // Filtramos los compromisos que estén en el rango de fechas especificado y que tengan estado true
                var gastos = await _context.Compromiso
                    .Include(p => p.Proveedor)
                    .Where(c => c.Fecha >= start && c.Fecha <= end && c.Estado)
                    .ToListAsync();

                // Filtramos solo aquellos compromisos cuyo proveedor tenga la propiedad SumaGastoMensual en true
                gastos = gastos.Where(c => c.Proveedor!.SumaGastoMensual).ToList();

                // Sumamos los montos de los gastos filtrados
                var totalGastos = gastos.Sum(c => c.Monto);

                return totalGastos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los gastos", ex);
            }
        }
    }
}
