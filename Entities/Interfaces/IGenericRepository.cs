﻿using System.Linq.Expressions;

namespace Entities.Interfaces
{
  public interface IGenericRepository<T> where T : class
  {
    T GetById(string id);
    T GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
  }
}
