using AutoMapper;
using MediatR;
using RegistrationWizard.Application.Countries.Dtos;
using RegistrationWizard.DataAccess.Abstractions;

namespace RegistrationWizard.Application.Countries.Queries.List;

public class ListCountriesQueryHandler(IUnitOfWork unOfWork, IMapper mapper) : IRequestHandler<ListCountryQuery, List<CountryDto>>
{
    public async Task<List<CountryDto>> Handle(ListCountryQuery request, CancellationToken cancellationToken)
    {
        var models = await unOfWork.CountryRepository.GetAsync();

        return mapper.Map<List<CountryDto>>(models);
    }
}
