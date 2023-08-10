using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EntityFramework;

public abstract class RepositoryBase<T> : IRepositoryBase<T>
where T: class, new()
{
    protected readonly StoreAppContext _context;

    public RepositoryBase(StoreAppContext context)
    {
        _context = context;
    }
    public IQueryable<T> FindAll(bool trackChanges)
    {
        return trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();
    }

    public T? FindByCondition(Expression<Func<T, bool>> filter, bool trackChanges)
    {
        return trackChanges
            ? _context.Set<T>().Where(filter).SingleOrDefault()
            : _context.Set<T>().AsNoTracking().Where(filter).SingleOrDefault();
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}