using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Sepehr.Application.Interfaces;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.LogEntities;
using System.Drawing;
using System.Reflection.Emit;

namespace Sepehr.Infrastructure.Persistence.Context
{
    public class ApplicationLogDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationLogDbContext(DbContextOptions<ApplicationLogDbContext> options, 
            IDateTimeService dateTime, 
            IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<ProductLog> ProductLogs { get; set; }
        public DbSet<ProductDetailLog> ProductDetailLogs { get; set; }
        public DbSet<ProductPriceLog> ProductPriceLogs { get; set; }
        public DbSet<CustomerLog> CustomerLogs { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }
        public DbSet<OrderDetailLog> OrderDetailLogs { get; set; }
        public DbSet<LadingLicenseDetailLog> LadingLicenseDetailLogs { get; set; }
        public DbSet<ProductInventoryLog> ProductInventorieLogs { get; set; }
        public DbSet<CargoAnnouncementLog> CargoAnnouncementLogs { get; set; }
        public DbSet<ProductSupplierLog> ProductSupplierLogs { get; set; }
        public DbSet<ReceivePayLog> ReceivePayLogs { get; set; }
        public DbSet<ReceivePaymentOriginLog> ReceivePaymentSourceLogs { get; set; }
        public DbSet<ProductBrandLog> ProductBrandLogs { get; set; }
        public DbSet<OrderPaymentLog> OrderPaymentLogs { get; set; }
        public DbSet<OrderServiceLog> OrderServiceLogs { get; set; }
        public DbSet<TableRecordRemovalInfo> TableRecordRemovalInfoLogs { get; set; }
        public DbSet<SepehrLog> SepehrLogs { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntityLog<int>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntityLog<Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }

                entry.Property(p => p.Created).IsModified = false;
                entry.Property(p => p.CreatedBy).IsModified = false;

            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("seplog");
            var cascadeEns = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach(var fk in cascadeEns) { 
                fk.DeleteBehavior=DeleteBehavior.Restrict; 
            }

            base.OnModelCreating(builder);

        }
    }
}