using AutoMapper;
using Tools.Data.Entities;
using Tools.Shared.DTOs.Producto;

namespace Tools.Service.Mappings
{
    public class ProductoDTOMapping : Profile
    {
        public ProductoDTOMapping()
        {
            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<Producto, Producto2DTO>();
        }
    }
}
