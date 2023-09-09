using Tools.Shared.DTOs.Producto;

namespace Tools.Shared.DTOs.Proveedor
{
    public class ProveedorWithProductsDTO
    {
        public int? IdProveedor { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool SumaGastoMensual { get; set; }
        public List<Producto2DTO>? Productos { get; set; }
    }
}
