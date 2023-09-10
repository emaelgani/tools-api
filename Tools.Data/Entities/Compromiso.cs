namespace Tools.Data.Entities
{
    public class Compromiso
    {

        public int IdCompromiso { get; set; }
        public int IdProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public decimal Monto { get; set; }
        public decimal? PagoEfectivo { get; set; }
        public decimal? PagoDigital { get; set; }

        //Propiedad de navegación.
        public Proveedor? Proveedor { get; set; }
    }
}
