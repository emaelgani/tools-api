namespace Tools.Shared.DTOs.Pago
{
    public class PagoListDTO
    {
        public int IdPago { get; set; }
        public int IdCliente { get; set; }
        public int IdMetodoPago { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalPago { get; set; }
        public string? Cliente { get; set; }
        public string? MetodoPago { get; set; }
    }
}
