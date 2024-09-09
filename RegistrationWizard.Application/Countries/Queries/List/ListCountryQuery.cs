using MediatR;
using RegistrationWizard.Application.Countries.Dtos;

namespace RegistrationWizard.Application.Countries.Queries.List;

public record ListCountryQuery : IRequest<List<CountryDto>>;