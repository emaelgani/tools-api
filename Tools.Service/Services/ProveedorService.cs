using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tools.Data.Entities;
using Tools.Data.Interfaces;
using Tools.Service.DTOs.Proveedor;
using Tools.Service.Exceptions;
using Tools.Service.Interfaces;

namespace Tools.Service.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IMapper _mapper;
        private readonly IProveedorRepository _proveedorRepo;

        public ProveedorService(IMapper mapper, IProveedorRepository proveedorRepo)
        {
            _mapper = mapper;
            _proveedorRepo = proveedorRepo;
        }
        public async Task Add(ProveedorDTO newProveedor)
        {
            try
            {
                var proveedor = _mapper.Map<Proveedor>(newProveedor);
                _proveedorRepo.Add(proveedor);
                await _proveedorRepo.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding a proveedor.", ex);
            }
        }

        public Task AumentarPorcentajeProveedor(ProveedorAumentoDTO proveedorAumento)
        {
            throw new NotImplementedException();
        }

        public Task CreatePdfListaPrecios(int idProveedor)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ProveedorDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task Update(ProveedorDTO updateProveedor)
        {
            try
            {
                var proveedor =  _proveedorRepo.FindByCondition(p => p.IdProveedor == updateProveedor.IdProveedor).FirstOrDefault();
                if (proveedor is null)
                {
                    throw new NotFoundException("Proveedor not found.");
                }

                proveedor.Nombre = updateProveedor.Nombre;
                proveedor.Telefono = updateProveedor.Telefono;
                proveedor.Descripcion = updateProveedor.Descripcion;
                proveedor.SumaGastoMensual = updateProveedor.SumaGastoMensual;

                await _proveedorRepo.CommitAsync();

            }
            catch(NotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating a proveedor.", ex);
            }
        }
    }
}
