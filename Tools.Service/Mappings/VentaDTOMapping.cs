using AutoMapper;
using Tools.Data.Entities;
using Tools.Shared.DTOs.Cliente;
using Tools.Shared.DTOs.Venta;

namespace Tools.Service.Mappings
{
    public class VentaDTOMapping : Profile
    {
        public VentaDTOMapping()
        {
            CreateMap<VentaDTO, Venta>();
            CreateMap<VentaProductoDTO, VentaProducto>();
        }
    }
}
