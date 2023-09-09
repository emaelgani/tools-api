namespace Tools.Data.Entities
{
    public class TipoPrecio
    {
        public int IdTipoPrecio { get; set; }

        //CONTADO - FINANCIADO.
        public string Tipo { get; set; } = string.Empty;

        public List<VentaProducto>? VentaProductos { get; set; }

    }
}
