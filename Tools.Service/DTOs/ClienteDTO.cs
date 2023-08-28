﻿namespace Tools.Service.DTOs
{
    public class ClienteDTO
    {
        public int? IdCliente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public decimal Deuda { get; set; }
    }
}
