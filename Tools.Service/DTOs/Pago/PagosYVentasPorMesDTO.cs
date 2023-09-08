namespace Tools.Service.DTOs.Pago
{
    public class PagosYVentasPorMesDTO
    {
        public List<PagoPorMesDTO> Pagos { get; set; } = new List<PagoPorMesDTO>();
        public List<VentaPorMesDTO> Ventas { get; set; } = new List<VentaPorMesDTO>();
    }
}
