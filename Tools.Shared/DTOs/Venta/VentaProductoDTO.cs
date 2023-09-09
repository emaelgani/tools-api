namespace Tools.Shared.DTOs.Venta
{
    public class VentaProductoDTO
    {
        public int? IdVentaProducto { get; set; }

        public int? IdVenta { get; set; }

        public int IdProducto { get; set; }

        public int IdTipoPrecio { get; set; }

        public int Cantidad { get; set; }

        public bool EsPrecioEspecial { get; set; }

        public decimal PrecioUnidad { get; set; }

        public decimal Total { get; set; }

        public string? TipoPrecio { get; set; }

        public string? Producto { get; set; }

        public string? Proveedor { get; set; }

        public string? Descripcion { get; set; }
    }
}
