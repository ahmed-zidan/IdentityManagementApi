using Api.Dtos;
using AutoMapper;
using Core.Identity;

namespace Api.Helpers
{
    public class MyMapper:Profile
    {
        public MyMapper()
        {
            CreateMap<RegisterDto, AppUser>()
                .ForMember(x=>x.Email , y=>y.MapFrom(src=>src.Email.ToLower()))
                .ForMember(x => x.EmailConfirmed, y=>y.MapFrom(src=>true))
                .ForMember(x=>x.UserName , y=>y.MapFrom<AppUserResolver>());
        }
    }
}
