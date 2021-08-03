using System;
using System.Linq;
using System.Threading.Tasks;
using MyWashApi.Data.Models;

namespace MyWashApi.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly MyWashContext _ctx;

        public Repository(MyWashContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _ctx.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null.");
            }

            try
            {
                await _ctx.AddAsync(entity);
                await _ctx.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}.");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null.");
            }

            try
            {
                _ctx.Update(entity);
                await _ctx.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}.");
            }
        }

        public async Task Delete(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            try
            {
                _ctx.Remove(entity);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be deleted: {ex.Message}.");
            }
        }
    }
}