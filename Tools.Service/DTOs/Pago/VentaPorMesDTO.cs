namespace Tools.Service.DTOs.Pago
{
    public class VentaPorMesDTO
    {
        public string? NombreProducto { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal TotalVentas { get; set; }
    }
}
