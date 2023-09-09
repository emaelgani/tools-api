namespace Tools.Shared.DTOs.Venta
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalVenta { get; set; }
        public string? Cliente { get; set; }
    }
}
