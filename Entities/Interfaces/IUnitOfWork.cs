namespace Entities.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    int Save();
  }
}
