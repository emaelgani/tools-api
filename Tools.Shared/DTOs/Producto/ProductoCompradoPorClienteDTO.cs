namespace Tools.Shared.DTOs.Producto
{
    public class ProductoCompradoPorClienteDTO
    {
        public string NombreCliente { get; set; } = String.Empty;
        public string NombreProducto { get; set; } = String.Empty;
        public int CantidadProductos { get; set; }
    }
}
