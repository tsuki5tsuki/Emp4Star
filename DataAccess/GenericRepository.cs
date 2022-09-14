using Entities.Interfaces;
using System.Linq.Expressions;

namespace DataAccess
{
  public class GenericRepository<T> : IGenericRepository<T> where T : class
  {
    protected readonly Context context;
    public GenericRepository(Context context)
    {
      this.context = context;
    }
    public T GetById(string id)
    {
      return context.Set<T>().Find(id);
    }
    public T GetById(int id)
    {
      return context.Set<T>().Find(id);
    }
    public IEnumerable<T> GetAll()
    {
      return context.Set<T>().ToList();
    }
    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
      return context.Set<T>().Where(expression);
    }
    public void Add(T entity)
    {
      context.Set<T>().Add(entity);
    }
    public void AddRange(IEnumerable<T> entities)
    {
      context.Set<T>().AddRange(entities);
    }
    public void Update(T entity)
    {
      context.Set<T>().Update(entity);
    }
    public void Remove(T entity)
    {
      context.Set<T>().Remove(entity);
    }
    public void RemoveRange(IEnumerable<T> entities)
    {
      context.Set<T>().RemoveRange(entities);
    }
  }
}
