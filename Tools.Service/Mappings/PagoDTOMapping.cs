using AutoMapper;
using Tools.Data.Entities;
using Tools.Service.DTOs.Pago;

namespace Tools.Service.Mappings
{
    public class PagoDTOMapping : Profile
    {
        public PagoDTOMapping()
        {
            CreateMap<Pago, PagoDTO>().ReverseMap();
        }
    }
}
