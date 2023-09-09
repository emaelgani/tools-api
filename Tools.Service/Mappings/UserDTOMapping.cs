using AutoMapper;
using Tools.Data.Entities;
using Tools.Shared.DTOs.User;

namespace Tools.Service.Mappings
{
    public class UserDTOMapping : Profile
    {
        public UserDTOMapping()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
