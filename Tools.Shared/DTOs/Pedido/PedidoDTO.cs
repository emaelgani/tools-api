namespace Tools.Shared.DTOs.Pedido
{
    public class PedidoDTO
    {
        public int? IdPedido { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string Descripcion { get; set; } = string.Empty;
       
    }
}
