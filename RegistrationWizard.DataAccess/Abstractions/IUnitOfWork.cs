namespace RegistrationWizard.DataAccess.Abstractions;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ICountryRepository CountryRepository { get; }
    IProvinceRepository ProvinceRepository { get; }
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task SaveAsync();
}
