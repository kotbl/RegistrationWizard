using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.DataAccess.Abstractions;

public interface IUserRepository : IAbstractRepository<UserModel>
{
    Task<bool> CheckExistsUserByEmail(string email);
}
