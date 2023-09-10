using Tools.Shared.DTOs.Proveedor;

namespace Tools.Shared.DTOs.Compromiso
{
    public class CompromisoDTO
    {
        public int IdCompromiso { get; set; }
        public int IdProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public decimal Monto { get; set; }
        public decimal? PagoEfectivo { get; set; }
        public decimal? PagoDigital { get; set; }
        public ProveedorDTO? Proveedor { get; set; }

    }
}
