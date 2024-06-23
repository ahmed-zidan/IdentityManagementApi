using Api.Dtos;
using AutoMapper;
using AutoMapper.Execution;
using Core.Identity;

namespace Api.Helpers
{
    public class AppUserResolver : IValueResolver<RegisterDto, AppUser, string>
    {
        public string Resolve(RegisterDto source, AppUser destination, string destMember, ResolutionContext context)
        {
            if (source.FirstName != null || source.LastName != null) {
                return source.FirstName + "_" + source.LastName;
            }
            else
            {
                return "";
            }
        }
    }
}
