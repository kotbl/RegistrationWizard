using RegistrationWizard.DataAccess.Abstractions;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.DataAccess.Repositories;

public class CountryRepository(AppDbContext context) : AbstractRepository<CountryModel>(context), ICountryRepository;
