using AutoMapper;
using Tools.Data.Entities;
using Tools.Shared.DTOs.Pago;

namespace Tools.Service.Mappings
{
    public class PagoDTOMapping : Profile
    {
        public PagoDTOMapping()
        {
            CreateMap<PagoAddDTO,Pago>();
        }
    }
}
