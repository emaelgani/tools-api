namespace Tools.Shared.DTOs.Proveedor
{
    public class ProveedorDTO
    {
        public int? IdProveedor { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool SumaGastoMensual { get; set; }
    }
}
