using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;
using Stimulsoft.Blockly.Model;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> table = null;

        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
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

        public async Task<IEnumerable<TEntity>> LoadAllWithRelatedAsync<TEntity>(
            params Expression<Func<TEntity, object>>[] expressionList) where TEntity : class
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return query
                .AsNoTracking();
        }

        public async Task<IQueryable<TEntity>> GetAllWithRelatedAsQueryble<TEntity>(
            params Expression<Func<TEntity, object>>[] expressionList) where TEntity : class
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return query.AsQueryable();
        }

        public async Task<TEntity?> LoadSingleWithRelatedAsync<TEntity>(
            TEntity entity, 
            Guid id,
            params Expression<Func<TEntity,
                object>>[] expressionList) where TEntity : AuditableBaseEntity<Guid>
        {
            var query = _dbContext.Set<TEntity>().Where(d=>d.Id==id).AsQueryable();
            foreach (var expression in expressionList)
            {
                query = query.Include(expression);
            }

            return await query.FirstOrDefaultAsync();
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

        public async Task<List<T>> UpdateAsync(List<T> entity)
        {
            _dbContext.Set<T>().UpdateRange(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            //table.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity,T oldEntity)
        {
            _dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbContext
                 .Set<T>()
                 .AsQueryable();
        }

    }


}