namespace Tools.Shared.DTOs
{
    //Se genera un DTO en un proyecto de biblioteca compartida.
    //Objeto: Capa DATA necesita acceder a DTO que esta declarado en la capa SERVICE - evitar acoplamiento.
    public class Producto2DTO
    {
        public int IdProducto { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioContado { get; set; }
        public decimal PrecioFinanciado { get; set; }
        public decimal PrecioLista { get; set; }
        public int Stock { get; set; }
        public int CantidadVentas { get; set; }
        public string NombreProveedor { get; set; } = string.Empty;
        public int IdProveedor { get; set; }
        public DateTime Actualizado { get; set; }
    }
}