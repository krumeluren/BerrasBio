using AutoMapper;
using BerrasBio.Models;
using BerrasBio.ViewModels;

namespace BerrasBio.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
