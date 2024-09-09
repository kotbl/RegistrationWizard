using AutoMapper;
using RegistrationWizard.Application.Countries.Dtos;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.Application.Countries.MappingConfig;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<CountryModel, CountryDto>();
    }
}