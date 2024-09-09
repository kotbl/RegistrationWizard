using AutoMapper;
using RegistrationWizard.Application.Provinces.Dtos;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.Application.Provinces.MappingConfig;

public class ProvinceProfile : Profile
{
    public ProvinceProfile()
    {
        CreateMap<ProvinceModel, ProvinceDto>();
    }
}