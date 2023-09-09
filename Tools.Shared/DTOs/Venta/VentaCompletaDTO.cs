namespace Tools.Shared.DTOs.Venta
{
    public class VentaCompletaDTO
    {
        public VentaDTO Venta { get; set; } = new VentaDTO();
        public List<VentaProductoDTO> Productos { get; set; } = new List<VentaProductoDTO>();
    }
}
