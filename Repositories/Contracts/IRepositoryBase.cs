using System.Linq.Expressions;

namespace Repositories.Contracts;

public interface IRepositoryBase<T>
{ 
    IQueryable<T> FindAll(bool trackChanges);
    T? FindByCondition(Expression<Func<T, bool>> filter,bool trackChanges);

    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);

}