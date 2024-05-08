using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces;
using Sepehr.Domain.Common;
using Sepehr.Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class GenericRepositoryLogAsync<T> : IGenericRepositoryLogAsync<T> where T : class
    {
        private readonly ApplicationLogDbContext _dbContext;

        public GenericRepositoryLogAsync(ApplicationLogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<IEnumerable<TEntity>> LoadAllWithRelatedAsync<TEntity>(
            int pageNumber, int pageSize,
            params Expression<Func<TEntity, object>>[] expressionList) where TEntity : class
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IQueryable<TEntity>> LoadAllWithRelatedAsQueryableAsync<TEntity>(
            int pageNumber, int pageSize,
            params Expression<Func<TEntity, object>>[] expressionList) where TEntity : class
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<TEntity> LoadSingleWithRelatedAsync<TEntity>(TEntity entity, 
            params Expression<Func<TEntity, object>>[] expressionList) where TEntity : AuditableBaseEntity<Guid>
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetPagedReponseWithRelatedIntAsync<TEntity>(TEntity entity,
            params Expression<Func<TEntity, object>>[] expressionList) where TEntity : BaseEntity<int>
        {
            if (entity == null)
                return null;

            var query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == entity.Id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> AddAsync(List<T> entity)
        {
            await _dbContext.Set<T>().AddRangeAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var brands= await _dbContext
                 .Set<T>()
                 .ToListAsync();
            return brands;
        }

    }
}