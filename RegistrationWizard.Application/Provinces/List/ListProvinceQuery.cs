using MediatR;
using RegistrationWizard.Application.Provinces.Dtos;

namespace RegistrationWizard.Application.Provinces.List;

public class ListProvinceQuery : IRequest<List<ProvinceDto>>
{
    public int CountryId { get; set; }
}