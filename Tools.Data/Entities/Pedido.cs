namespace Tools.Data.Entities
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }

        //Navigation properties
        public Cliente? Cliente { get; set; } 
    }
}
