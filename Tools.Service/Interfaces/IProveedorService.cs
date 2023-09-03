using Tools.Service.DTOs.Proveedor;

namespace Tools.Service.Interfaces
{
    public interface IProveedorService
    {
        Task<IList<ProveedorDTO>> GetAllAsync();
        Task Update(ProveedorDTO updateProveedor);
        Task Add(ProveedorDTO newProveedor);
        Task AumentarPorcentajeProveedor(ProveedorAumentoDTO proveedorAumento);
        Task CreatePdfListaPrecios(int idProveedor);
    }
}
