using Tools.Shared.DTOs.Pago;

namespace Tools.Service.Interfaces
{
    public interface IPagoService
    {
        public Task Add(PagoAddDTO pagoDto);
        public Task<IList<PagoListDTO>> GetAllAsync();
        public Task<IList<PagoPorMesDTO>> GetPagosPorMes();
        public Task<PagosYVentasPorMesDTO> GetPagosYVentasPorMes();
        public Task<decimal> GetLiquidezEfectivo();
        public Task<decimal> GetLiquidezDigital();
        Task DeletePago(int idPago);
    }
}
