namespace Tools.Data.Entities
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdCliente { get; set; }
        public int IdMetodoPago { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalPago { get; set; }

        // Propiedades de navegación
        public Cliente? Cliente { get; set; }
        public MetodoPago? MetodoPago { get; set; }
    }
}
