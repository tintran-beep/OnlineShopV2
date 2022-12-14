using Microsoft.EntityFrameworkCore.Storage;
using OnlineShop.Data.Context;

namespace OnlineShop.Data.Infrastructure
{
    public interface IUnitOfWork<TContext> : IDisposable, IAsyncDisposable where TContext : BaseDbContext
    {
        Task SaveChangesAsync();
        IRepository<TContext, TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    }

    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : BaseDbContext
    {
        private TContext _dbContext;

        private IDbContextTransaction _transaction;

        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TContext, TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var entityType = typeof(TEntity);

            if (_repositories.ContainsKey(entityType) == false)
            {
                var repositoryInstance = new Repository<TContext, TEntity>(_dbContext);
                _repositories.Add(entityType, repositoryInstance);
            }
            return (Repository<TContext, TEntity>)_repositories[entityType];
        }

        public virtual async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        #region Implement Dispose Object
        public virtual void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext is not null)
                    _dbContext.Dispose();

                _dbContext = null;
            }
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_dbContext is not null)
            {
                await _dbContext.DisposeAsync().ConfigureAwait(false);
            }
            _dbContext = null;
        }
        #endregion
    }
}
