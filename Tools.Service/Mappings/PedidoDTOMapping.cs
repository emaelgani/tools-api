using AutoMapper;
using Tools.Data.Entities;
using Tools.Shared.DTOs.Pedido;

namespace Tools.Service.Mappings
{
    public class PedidoDTOMapping : Profile
    {
        public PedidoDTOMapping()
        {
            CreateMap<Pedido, PedidoDTO>().ReverseMap();
            CreateMap<Pedido, PedidoWithClientDTO>();
        }
    }
}
