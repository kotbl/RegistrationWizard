using AutoMapper;
using RegistrationWizard.Application.Users.Commands.Create;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.Application.Users.MappingConfig;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateCommand, UserModel>()
            .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Login))
            .ForMember(dst => dst.Password, opt => opt.Ignore());
    }
}