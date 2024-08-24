using Sepehr.Domain.Common;
using Sepehr.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T: class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        IQueryable<T> GetAllAsQueryable();
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<TEntity?> LoadSingleWithRelatedAsync<TEntity>(Guid Id,params Expression<Func<TEntity, object>>[] expressionList) where TEntity : AuditableBaseEntity<Guid>;
        Task<IQueryable<TEntity>> LoadAllWithRelatedAsQueryableAsync<TEntity>(int pageNumber, int pageSize,params Expression<Func<TEntity, object>>[] expressionList) where TEntity : class;
        Task<IEnumerable<TEntity>> LoadAllWithRelatedAsync<TEntity>(int pageNumber, int pageSize,
            params Expression<Func<TEntity, object>>[] expressionList) where TEntity : class;
        Task<IEnumerable<TEntity>> LoadAllWithRelatedAsync<TEntity>(
            params Expression<Func<TEntity, object>>[] expressionList) where TEntity : class;
        Task<T> AddAsync(T entity);
        Task<List<T>> AddAsync(List<T> entity);
        Task UpdateAsync(T entity);
        Task UpdateAsync(T entity, T oldEntity);
        Task<List<T>> UpdateAsync(List<T> entity);
        Task DeleteAsync(T entity);
        Task<CustomerViewModel> GetCustomerAccountInfo(Guid Id);
    }
}