using Tools.Data.Entities;
using Tools.Shared.DTOs.Pago;

namespace Tools.Data.Interfaces
{
    public interface IPagoRepository : IGenericRepository<Pago>
    {
        public Task<decimal> GetLiquidezDigital();
        public Task<decimal> GetLiquidezEfectivo();
        public new Task<IList<PagoListDTO>> GetAllAsync();
        public Task<IList<PagoPorMesDTO>> GetPagosPorMes();
        public Task<PagosYVentasPorMesDTO> GetPagosYVentasPorMes();
    }
}
