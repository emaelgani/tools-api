using AutoMapper;
using Tools.Data.Entities;
using Tools.Service.DTOs.Cliente;

namespace Tools.Service.Mappings
{
    public class ClienteDTOMapping : Profile
    {
        public ClienteDTOMapping() {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
        }
    }
}
