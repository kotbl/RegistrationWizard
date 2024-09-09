using System.Linq.Expressions;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.DataAccess.Abstractions;

public interface IAbstractRepository<TModel> where TModel : BaseModel
{
    Task<IList<TModel>> GetAsync(
        Expression<Func<TModel, bool>>? filter = null,
        Func<IQueryable<TModel>, IOrderedQueryable<TModel>>? orderBy = null,
        string includeProperties = "");
    Task<TModel?> GetByIdAsync(int id, string includeProperties = "");
    Task InsertAsync(TModel entity);
    Task InsertRangeAsync(IEnumerable<TModel> entity);
    Task DeleteAsync(int id);
}
