﻿using Microsoft.AspNetCore.Mvc;
using Tools.Service.DTOs.Proveedor;

namespace Tools.Service.Interfaces
{
    public interface IProveedorService
    {
        Task<IList<ProveedorWithProductsDTO>> GetAllAsync();
        Task Update(ProveedorDTO updateProveedor);
        Task Add(ProveedorDTO newProveedor);
        Task AumentarPorcentajeProveedor(ProveedorAumentoDTO proveedorAumento);
        Task<IActionResult> CreatePdfListaPrecios(int idProveedor);
    }
}
