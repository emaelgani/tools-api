namespace Tools.Data.Entities
{
    public class VentaProducto
    {
        public int IdVentaProducto { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int IdTipoPrecio { get; set; }
        public int Cantidad { get; set; }
        public bool EsPrecioEspecial { get; set; }
        public decimal PrecioUnidad { get; set; }
        public decimal Total { get; set; }


        //Propiedades de navegacion
        public TipoPrecio? TipoPrecio { get; set; }
        public Producto? Producto { get; set; }
        public Venta? Venta { get; set; }
    }
}
