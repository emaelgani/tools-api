using Microsoft.AspNetCore.Mvc;
using Tools.Shared.DTOs.Proveedor;

namespace Tools.Service.Interfaces
{
    public interface IProveedorService
    {
        Task<IList<ProveedorWithProductsDTO>> GetAllAsync();
        Task Update(ProveedorDTO updateProveedor);
        Task Add(ProveedorDTO newProveedor);
        Task AumentarPorcentajeProveedor(ProveedorAumentoDTO proveedorAumento);
        Task<IActionResult> CreatePdfListaPrecios(int idProveedor);
        Task<ProveedorDTO?> GetById(int id);
        Task<IList<ProveedorDTO>> GetAsync();
    }
}
