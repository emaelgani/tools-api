namespace Tools.Shared.DTOs.Cliente
{
    public class ClienteVentaProductoDTO : ClienteDTO
    {
        public DateTime? UltimaVenta { get; set; }
        public int CantidadVendidos { get; set; }
    }
}
