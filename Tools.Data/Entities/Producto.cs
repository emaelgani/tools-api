namespace Tools.Data.Entities
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Marca { get; set; } = string.Empty;
        public string? Nombre { get; set; } = string.Empty;
        public string? Codigo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioContado { get; set; }
        public decimal PrecioFinanciado { get; set; }
        public decimal PrecioLista { get; set; }
        public int Stock { get; set; }
        public int IdProveedor { get; set; }
        public DateTime Actualizado { get; set; }

        //Navigation properties
        public Proveedor? Proveedor { get; set; }
        public List<VentaProducto>? VentaProductos { get; set; }

    }
}
