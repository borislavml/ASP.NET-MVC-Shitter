namespace Shitter.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Shitter.Models;

    public interface IShitterDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
