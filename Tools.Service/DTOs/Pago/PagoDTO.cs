namespace Tools.Service.DTOs.Pago
{
    public class PagoDTO
    {
        public int IdPago { get; set; }
        public int IdCliente { get; set; }
        public int IdMetodoPago { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalPago { get; set; }
    }
}
