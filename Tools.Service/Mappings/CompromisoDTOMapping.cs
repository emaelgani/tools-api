using AutoMapper;
using Tools.Data.Entities;
using Tools.Shared.DTOs.Compromiso;

namespace Tools.Service.Mappings
{
    public class CompromisoDTOMapping : Profile
    {
        public CompromisoDTOMapping()
        {
            CreateMap<CompromisoDTO, Compromiso>().ReverseMap();
        }
    }
}
