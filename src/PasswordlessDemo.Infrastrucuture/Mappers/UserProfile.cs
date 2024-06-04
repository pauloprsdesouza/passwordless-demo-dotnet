using AutoMapper;
using PasswordlessDemo.Contracts.Users;
using PasswordlessDemo.Domain.Users;

namespace PasswordlessDemo.Infrastructure.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            _ = CreateMap<SignInRequest, User>();

            _ = CreateMap<SignUpRequest, User>();

            _ = CreateMap<UpdateUserRequest, User>();

            _ = CreateMap<User, UserResponse>()
                .ForPath(dest => dest.Name, opts => opts.MapFrom(src => src.ToString()));
        }
    }
}
