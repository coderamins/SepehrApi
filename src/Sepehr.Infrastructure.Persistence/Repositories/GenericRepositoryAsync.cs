using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;
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

        public async Task<TEntity?> LoadSingleWithRelatedAsync<TEntity>(Guid id,
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

        public async Task<CustomerViewModel> GetCustomerAccountInfo(Guid CustId)
        {
            // بدهکاری مشتری
            //-----لیست سفارشات فروش به مشتری ------
            var cust_orders =await _dbContext.Set<Order>().Where(o=>o.CustomerId==CustId).ToListAsync();

            //----لیست سفارشاتی که خروج داشته اند-----
            var cust_exited_cargo = await _dbContext.Set<Order>()
                                .Include(x => x.LadingPermits.Where(x => x.LadingExitPermit != null))
                                    .ThenInclude(x => x.LadingExitPermit)
                                .Include(x => x.LadingPermits)
                                    .ThenInclude(x => x.CargoAnnounce)
                                .Where(o => o.LadingPermits.Count() > 0)
                                .ToListAsync();
            // بستانکاری مشتری
            //-----لیست سفارشات خرید از مشتری ------
            var purchase_orders= await _dbContext.Set<PurchaseOrder>().Where(o=>o.CustomerId == CustId).ToListAsync();

            //-----لیست سفارشاتی که تخلیه بار شده اند------
            var cust_unloaded_orders = await _dbContext.Set<PurchaseOrder>()
                    .Include(x => x.TransferRemittances.Where(x => x.EntrancePermit != null && x.EntrancePermit.UnloadingPermit!=null))
                        .ThenInclude(x => x.EntrancePermit)
                        .ThenInclude(x => x.UnloadingPermit)
                    .ToListAsync();


            #region مانده بستانکاری مشتری
            //-----لیست پرداخت های بازرگانی به مشتری ------
            var receive_payments = await _dbContext.Set<ReceivePay>()
                .Where(r=>r.PayToCustomerId==CustId && r.ReceivePayStatusId==(int)EReceivePayStatus.AccApproved).ToListAsync();
            
            var cust_pay_requests=await _dbContext.Set<PaymentRequest>()
                .Where(x=>x.CustomerId==CustId && x.PaymentRequestStatusId==(int)EPaymentRequestStatus.Payed).ToListAsync();

            #endregion

            decimal cust_creditor = (purchase_orders.Sum(o => o.TotalAmount) +
                                    receive_payments.Sum(x => x.Amount));
            decimal dept =
                (cust_orders.Sum(c => c.TotalAmount) +
                cust_pay_requests.Sum(x => x.Amount));


            return new CustomerViewModel { 
                CustomerDept = dept,
                CustomerCreditor=cust_creditor,
                CustomerCurrentDept= dept - cust_creditor
            };
        }

    }


}