using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DataAccess.Abstractions;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.DataAccess.Repositories;

public class UserRepository(AppDbContext context) : AbstractRepository<UserModel>(context), IUserRepository
{
    public async Task<bool> CheckExistsUserByEmail(string email)
    {
        email = email.Trim().ToLower();

        return await DbSet.AnyAsync(x => x.Email.Trim().ToLower() == email);
    }
}
