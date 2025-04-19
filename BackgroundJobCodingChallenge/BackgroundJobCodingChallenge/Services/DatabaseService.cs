using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BackgroundJobCodingChallenge.Services
{
    public class DatabaseService : IDatabaseService
    {
        public Task<TResult> GetAsync<TEntity, TResult>(FCreateQuery<TEntity, TResult> createQuery)
        {
            // Stubbed implementation
            return Task.FromResult(default(TResult));
        }

        public Task<TEntity> CreateAsync<TEntity>(TEntity entities)
        {
            // Stubbed implementation
            return Task.FromResult(entities);
        }

        public Task<TEntity> CreateAsync<TEntity>(IEnumerable<TEntity> entities)
        {
            // Stubbed implementation
            return Task.FromResult(default(TEntity));
        }

        public Task<TEntity> UpdateAsync<TEntity>(TEntity entities)
        {
            // Stubbed implementation
            return Task.FromResult(entities);
        }

        public Task<TEntity> UpdateAsync<TEntity>(IEnumerable<TEntity> entities)
        {
            // Stubbed implementation
            return Task.FromResult(default(TEntity));
        }

        public Task DeleteAsync<TEntity>(TEntity entities)
        {
            // Stubbed implementation
            return Task.CompletedTask;
        }

        public Task DeleteAsync<TEntity>(IEnumerable<TEntity> entities)
        {
            // Stubbed implementation
            return Task.CompletedTask;
        }
    }
}