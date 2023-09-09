namespace Tools.Shared.DTOs.Pago
{
    public class PagoAddDTO
    {
        public int IdCliente { get; set; }
        public int IdMetodoPago { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalPago { get; set; }
    }
}
