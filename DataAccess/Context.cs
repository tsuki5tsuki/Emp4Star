using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
  public class Context : DbContext
  {
    public Context(DbContextOptions options): base(options) { }

    //Automatically set created and modified date on each record in EF Core
    //https://www.entityframeworktutorial.net/faq/set-created-and-modified-date-in-efcore.aspx
    public override int SaveChanges()
    {
      var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

      foreach (var entityEntry in entries)
      {
        ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

        if (entityEntry.State == EntityState.Added)
        {
          ((BaseEntity)entityEntry.Entity).CreationDate = DateTime.Now;
        }
      }

      return base.SaveChanges();
    }
  }
}
