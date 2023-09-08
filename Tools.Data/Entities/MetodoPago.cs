namespace Tools.Data.Entities
{
    public class MetodoPago
    {
        public int IdMetodoPago { get; set; }

        // EFECTIVO - TRANSFERENCIA
        public string TipoMetodoPago { get; set; } = string.Empty;
    }
}
