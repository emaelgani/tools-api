﻿namespace Tools.Data.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public decimal Deuda { get; set; }

        public IList<Pedido>? Pedidos { get; set; }
        public IList<Venta>? Ventas { get; set; }
    }
}
