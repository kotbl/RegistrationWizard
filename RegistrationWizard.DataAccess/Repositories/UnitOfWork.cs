using Microsoft.EntityFrameworkCore.Storage;
using RegistrationWizard.DataAccess.Abstractions;

namespace RegistrationWizard.DataAccess.Repositories;

public class UnitOfWork(AppDbContext dbContext) : IDisposable, IUnitOfWork
{
    private IDbContextTransaction? _dbContextTransaction;

    private IUserRepository? _userRepository;
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(dbContext);

    private ICountryRepository? _countryRepository;
    public ICountryRepository CountryRepository => _countryRepository ??= new CountryRepository(dbContext);

    private IProvinceRepository? _provinceRepository;
    public IProvinceRepository ProvinceRepository => _provinceRepository ??= new ProvinceRepository(dbContext);

    public async Task BeginTransactionAsync() => _dbContextTransaction = await dbContext.Database.BeginTransactionAsync();

    public async Task CommitTransactionAsync()
    {

        if (_dbContextTransaction is null)
            return;

        await _dbContextTransaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        if (_dbContextTransaction is null)
            return;

        await _dbContextTransaction.RollbackAsync();
        await _dbContextTransaction.DisposeAsync();
    }

    public async Task SaveAsync() => await dbContext.SaveChangesAsync();

    private bool Disposed { get; set; }

    protected virtual void Dispose(bool disposing)
    {
        if (!Disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }

        Disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}