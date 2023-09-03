using System.ComponentModel.DataAnnotations;

namespace Tools.Data.Entities
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool SumaGastoMensual { get; set; } = false;

        // Navigation properties
        public List<Producto>? Productos { get; set; }
    }
}
