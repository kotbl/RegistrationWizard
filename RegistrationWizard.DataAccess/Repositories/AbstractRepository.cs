using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DataAccess.Abstractions;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.DataAccess.Repositories;

public class AbstractRepository<TModel> : IAbstractRepository<TModel> where TModel : BaseModel
{
    private const char IncludePropertySeparationSymbol = ',';
    protected AppDbContext Context { get; }
    protected DbSet<TModel> DbSet { get; }

    public AbstractRepository(AppDbContext context)
    {
        Context = context;
        DbSet = Context.Set<TModel>();
    }

    public virtual async Task<IList<TModel>> GetAsync(
        Expression<Func<TModel, bool>>? filter = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TModel> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                     (new[] { IncludePropertySeparationSymbol }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return orderBy != null
            ? await orderBy(query).AsNoTracking().ToListAsync()
            : await query.AsNoTracking().ToListAsync();
    }

    public virtual async Task<TModel?> GetByIdAsync(int id, string includeProperties = "")
    {
        IQueryable<TModel> query = DbSet;

        foreach (var includeProperty in includeProperties.Split
                     (new[] { IncludePropertySeparationSymbol }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    }

    public virtual async Task InsertAsync(TModel entity) =>
        await DbSet.AddAsync(entity);

    public virtual async Task InsertRangeAsync(IEnumerable<TModel> entity) =>
        await DbSet.AddRangeAsync(entity);

    public virtual async Task DeleteAsync(int id)
    {
        var entityToDelete = await DbSet.FindAsync(id);

        if (entityToDelete is null)
            return;

        if (Context.Entry(entityToDelete).State == EntityState.Detached)
        {
            DbSet.Attach(entityToDelete);
        }

        DbSet.Remove(entityToDelete);
    }
}
