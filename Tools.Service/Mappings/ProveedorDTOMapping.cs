using AutoMapper;
using Tools.Data.Entities;
using Tools.Shared.DTOs.Proveedor;

namespace Tools.Service.Mappings
{
    public class ProveedorDTOMapping : Profile
    {
        public ProveedorDTOMapping()
        {
            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();
            CreateMap<Proveedor, ProveedorWithProductsDTO>();
        }
    }
}
