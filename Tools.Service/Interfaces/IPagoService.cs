using Tools.Service.DTOs.Pago;

namespace Tools.Service.Interfaces
{
    public interface IPagoService
    {
        public Task Add(PagoDTO pagoDto);
        public Task<IList<PagoDTO>> GetAllAsync();
        public Task<IList<PagoPorMesDTO>> GetPagosPorMes();
        public Task<PagosYVentasPorMesDTO> GetPagosYVentasPorMes();
        public Task<decimal> GetLiquidezEfectivo();
        public Task<decimal> GetLiquidezDigital();
    }
}
