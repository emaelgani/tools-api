namespace Tools.Shared.DTOs.Producto
{
    public class ProductoDTO
    {
        public int IdProducto { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioContado { get; set; }
        public decimal PrecioFinanciado { get; set; }
        public decimal PrecioLista { get; set; }
        public int Stock { get; set; }
        public int IdProveedor { get; set; }
        public DateTime? Actualizado { get; set; }
        
    }
}
