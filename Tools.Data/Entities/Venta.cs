namespace Tools.Data.Entities
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalVenta { get; set; }

        //propiedad de navegacion
        public Cliente? Cliente { get; set; }
        public List<VentaProducto>? VentaProductos { get; set; }
    }
}
