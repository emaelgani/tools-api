namespace Tools.Service.DTOs.Producto
{
    public class ProductoPorVentaDTO
    {
        /// <summary>
        /// Se utiliza para el gráfico que muestra los 10 clientes que realizaron más compras.
        /// </summary>
        public string NombreCliente { get; set; } = string.Empty;
        public int CantidadProductos { get; set; }
        public int CantidadVentas { get; set; }
    }
}
