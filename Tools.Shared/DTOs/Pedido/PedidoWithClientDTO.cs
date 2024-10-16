﻿using Tools.Shared.DTOs.Cliente;

namespace Tools.Shared.DTOs.Pedido
{
    public class PedidoWithClientDTO
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public ClienteDTO Cliente { get; set; } = new();
    }
}
