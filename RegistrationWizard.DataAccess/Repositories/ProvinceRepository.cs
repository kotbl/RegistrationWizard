using RegistrationWizard.DataAccess.Abstractions;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.DataAccess.Repositories;

public class ProvinceRepository(AppDbContext context) : AbstractRepository<ProvinceModel>(context), IProvinceRepository;
