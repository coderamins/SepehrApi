﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Sepehr.Application.Interfaces;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Domain.Enums;
using Stimulsoft.Svg;
using System.Security.AccessControl;

namespace Sepehr.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;

            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<FarePaymentType> PaymentTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseType> WarehouseTypes { get; set; }
        public DbSet<OrderSendType> OrderSendTypes { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<CustomerValidity> CustomerValidities { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<CargoAnnounce> CargoAnnounces { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<InvoiceType> PurchaseInvoiceTypes { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ReceivePay> ReceivePays { get; set; }
        public DbSet<PaymentOriginType> PaymentOriginTypes { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductUnit> ProductUnits { get; set; }
        public DbSet<ProductStandard> ProductStandards { get; set; }
        public DbSet<ProductState> ProductStates { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<OrderPayment> OrderPayments { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<OrderService> OrderServices { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<CustomerOfficialCompany> CustomerOfficialCompanies { get; set; }
        public DbSet<OfficialWarehoseInventory> OfficialWarehoseInventories { get; set; }
        public DbSet<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
        public DbSet<PurchaseOrderService> PurchaseOrderServices { get; set; }
        public DbSet<ProductInventoryHistory> ProductInventoryHistories { get; set; }
        public DbSet<PurchaseOrderStatus> PurchaseOrderStatus { get; set; }
        public DbSet<PurchaseOrderTransfer> PurchaseOrderTransfers { get; set; }
        public DbSet<PurchaseOrderTransferDetail> PurchaseOrderTransferDetails { get; set; }
        public DbSet<CustomerWarehouse> CustomerWarehouses { get; set; }
        public DbSet<TransferRemittanceType> TransferRemittanceTypes { get; set; }
        public DbSet<TransferRemittance> TransferRemittances { get; set; }
        public DbSet<TransferRemittanceDetail> TransferRemittanceDetails { get; set; }
        public DbSet<TransferRemittanceStatus> TransferRemittanceStatus { get; set; }
        public DbSet<EntrancePermit> EntrancePermits { get; set; }
        public DbSet<UnloadingPermit> UnloadingPermits { get; set; }
        public DbSet<UnloadingPermitDetail> UnloadingPermitDetails { get; set; }
        public DbSet<ReceivePayStatus> ReceivePayStatus { get; set; }
        public DbSet<FareAmountStatus> FareAmountStatus { get; set; }
        public DbSet<ShareHolder> ShareHolders { get; set; }
        public DbSet<PettyCash> PettyCashs { get; set; }
        public DbSet<CashDesk> CashDesks { get; set; }
        public DbSet<OrganizationBank> OrganizationBanks { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<LadingExitPermitDetail> LadingExitPermitDetails { get; set; }
        public DbSet<RentPayment> RentPayments { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ApplicationMenu> ApplicationMenus { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<CargoAnnounceDetail> CargoAnnounceDetails { get; set; }
        public DbSet<OrderExitType> OrderExitTypes { get; set; }
        public DbSet<DriverFareAmountApprove> DriverFareAmountApproves { get; set; }
        public DbSet<PurchaseOrderFarePaymentType> PurchaseOrderFarePaymentTypes { get; set; }
        public DbSet<PurchaseOrderSendType> PurchaseOrderSendTypes { get; set; }
        public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public DbSet<Phonebook> Phonebook { get; set; }
        public DbSet<CustomerLabel> CustomerLabels { get; set; }
        public DbSet<CustomerAssignedLabel> CustomerAssignedLabels { get; set; }
        public DbSet<CustomerLabelType> CustomerLabelTypes { get; set; }
        public DbSet<PaymentRequestReason> PaymentRequestReasons { get; set; }
        public DbSet<PaymentRequestStatus> PaymentRequestStatus { get; set; }
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<PersonnelPaymentRequest> PersonnelPaymentRequests { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<TransferWarehouseInventory> TransferWarehouseInventories { get; set; }
        public DbSet<TransferWarehouseInventoryDetail> TransferWarehouseInventoryDetails { get; set; }
        public DbSet<RentPaymentDetail> RentPaymentDetails { get; set; }
        public DbSet<DraftOrder> DraftOrders { get; set; }
        public DbSet<OrderReturn> OrderReturn { get; set; }
        public DbSet<OrderDetailReturn> OrderDetailReturn { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<FiscalYear> FiscalYears { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableBaseEntity<int>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = Guid.Parse(_authenticatedUser.UserId ?? "465C2D61-95DB-4822-9E95-8571247296A6");
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<IAuditableBaseEntity<Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = Guid.Parse(_authenticatedUser.UserId);
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }

                entry.Property(p => p.Created).IsModified = false;
                entry.Property(p => p.CreatedBy).IsModified = false;

            }

            if (_authenticatedUser.UserId != null)
                BeforeSaveChanges();

            var saved = base.SaveChangesAsync(cancellationToken);
            return saved;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Univesal Query Filters
            builder.Entity<Product>().HasQueryFilter(p => p.IsActive);
            builder.Entity<Warehouse>().HasQueryFilter(p => p.IsActive);
            builder.Entity<WarehouseType>().HasQueryFilter(p => p.IsActive);
            builder.Entity<UserRole>().HasQueryFilter(p => p.IsActive);
            builder.Entity<Customer>().HasQueryFilter(p => p.IsActive);
            builder.Entity<Brand>().HasQueryFilter(p => p.IsActive);
            #endregion


            builder.HasDefaultSchema("sepdb");
            var cascadeEns = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeEns)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            builder.Entity<LadingPermit>()
            .HasIndex(p => new { p.CargoAnnounceId, p.IsActive }).IsUnique();

            builder.Entity<ProductInventory>()
            .HasIndex(p => new { p.ProductBrandId, p.WarehouseId }).IsUnique();

            builder.Entity<Warehouse>()
            .HasIndex(p => new { p.Name, p.WarehouseTypeId }).IsUnique();

            builder.Entity<Product>()
                .HasIndex(c => new { c.ProductCode }); // Index on IsEnabled column for better performance

            builder.Entity<InvoiceType>().ToTable("InvoiceTypes");

            builder.Entity<Order>().ToTable(t => t.HasTrigger("ordertrigger"));
            builder.Entity<Order>()
                .Property(o => o.OrderCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<PurchaseOrder>()
                .Property(o => o.OrderCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<CargoAnnounce>()
                .Property(o => o.CargoAnnounceNo)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<ShareHolder>()
                .Property(o => o.ShareHolderCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<LadingExitPermit>()
                .Property(o => o.LadingExitPermitCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<PaymentRequest>()
                .Property(o => o.PaymentRequestCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<RentPayment>()
                .Property(o => o.Id)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<DraftOrder>()
                .Property(o => o.DraftOrderCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<LadingExitPermit>().Property(prop => prop.LadingExitPermitCode)
                .UseIdentityColumn(100, 1);


            builder.Entity<Product>().Property(p => p.ProductStateId).HasDefaultValue(1);
            builder.Entity<Product>().Property(p => p.ProductStandardId).HasDefaultValue(1);
            builder.Entity<Product>().HasIndex(e => new { e.ProductCode }).IsUnique();

            builder.Entity<PurchaseOrderStatus>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<CustomerValidity>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<OrderStatus>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<PhoneNumberType>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<PaymentOriginType>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<ReceivePayStatus>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<TransferRemittanceStatus>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<Bank>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<InvoiceType>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<OrderExitType>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<PurchaseOrderFarePaymentType>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<PurchaseOrderSendType>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<PaymentRequestStatus>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<PaymentRequestReason>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<FareAmountStatus>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            builder.Entity<ReceivePay>()
                .Property(p => p.ReceivePayCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<Customer>()
                .Property(p => p.CustomerCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<Personnel>()
                .Property(p => p.PersonnelCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<EntrancePermit>()
                .Property(p => p.PermitCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<UnloadingPermit>()
                .Property(p => p.UnloadingPermitCode)
                .IsUnicode()
                .ValueGeneratedOnAdd();

            builder.Entity<ShareHolder>().Property(prop => prop.ShareHolderCode)
            .UseIdentityColumn(3030, 1);

            builder.Entity<EntrancePermit>().Property(prop => prop.PermitCode)
            .UseIdentityColumn(1001, 1);

            builder.Entity<Order>().Property(prop => prop.OrderCode)
            .UseIdentityColumn(10000, 2);

            builder.Entity<PurchaseOrder>().Property(prop => prop.OrderCode)
            .UseIdentityColumn(30001, 2);

            builder.Entity<PaymentRequest>().Property(prop => prop.PaymentRequestCode)
            .UseIdentityColumn(100, 1);

            builder.Entity<PersonnelPaymentRequest>().Property(prop => prop.PaymentRequestCode)
            .UseIdentityColumn(1000, 1);

            builder.Entity<Customer>().Property(prop => prop.CustomerCode)
            .UseIdentityColumn(100, 1);

            builder.Entity<Personnel>().Property(prop => prop.PersonnelCode)
            .UseIdentityColumn(1000, 1);

            builder.Entity<RentPayment>().Property(prop => prop.Id)
            .UseIdentityColumn(1010, 1);

            builder.Entity<DraftOrder>().Property(prop => prop.DraftOrderCode)
            .UseIdentityColumn(100, 1);

            builder.Entity<TransferRemittanceType>()
                .Property(p => p.Id)
                .IsUnicode()
                .ValueGeneratedNever();

            builder.Entity<CustomerLabelType>()
                .Property(p => p.Id)
                .IsUnicode()
                .ValueGeneratedNever();

            builder.Entity<WarehouseType>()
                .Property(p => p.Id)
                .IsUnicode()
                .ValueGeneratedNever();

            builder.Entity<ProductStandard>()
                .Property(p => p.Desc)
                .IsUnicode();

            builder.Entity<ProductState>()
                .Property(p => p.Desc)
                .IsUnicode();

            builder.Entity<Brand>()
                .Property(p => p.Name)
                .IsUnicode();

            builder.Entity<ApplicationUser>()
                .Property(p => p.UserName)
                .IsUnicode();

            builder.Entity<CargoAnnounce>().Property(u => u.CargoAnnounceNo).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<PaymentRequest>().Property(u => u.PaymentRequestCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<Order>().Property(u => u.OrderCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<PurchaseOrder>().Property(u => u.OrderCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<ReceivePay>().Property(u => u.ReceivePayCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<Product>().Property(u => u.ProductCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<Customer>().Property(u => u.CustomerCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<Personnel>().Property(u => u.PersonnelCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<ShareHolder>().Property(u => u.ShareHolderCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<LadingExitPermit>().Property(u => u.LadingExitPermitCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<UnloadingPermit>().Property(u => u.UnloadingPermitCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<DraftOrder>().Property(u => u.DraftOrderCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Entity<PersonnelPaymentRequest>().Property(u => u.PaymentRequestCode).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Entity<Customer>().HasMany(p => p.ReceivePaymentSourceFrom)
                    .WithOne(d => d.ReceiveFromCustomer)
                    .HasForeignKey(d => d.ReceiveFromCustomerId);

            builder.Entity<Customer>().HasMany(p => p.ReceivePaymentSourceTo)
                .WithOne(d => d.PayToCustomer)
                .HasForeignKey(d => d.PayToCustomerId);

            builder.Entity<Phonebook>()
                .HasIndex(p => new { p.CustomerId, p.PhoneNumber, p.PhoneNumberTypeId })
                .IsUnique();

            builder.Entity<Product>()
                .HasIndex(p => new { p.ProductName, p.ProductCode });

            builder.Entity<Brand>()
                .HasIndex(b => new { b.Name });

            builder.Entity<Warehouse>()
                .HasIndex(w => new { w.Name });

            builder.Entity<ApplicationUser>()
                .HasIndex(u => new { u.UserName });

            builder.Entity<RoleMenu>()
              .HasOne(rm => rm.ApplicationRole)
              .WithMany(r => r.RoleMenus)
              .HasForeignKey(rm => rm.ApplicationRoleId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany() // No inverse navigation property needed
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<RolePermission>()
                .HasOne(rp => rp.ApplicationRole)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.Details)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Attachment>()
            .HasOne(o => o.DraftOrder)
            .WithMany(o => o.Attachments)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CargoAnnounceDetail>()
            .HasOne(od => od.CargoAnnounce)
            .WithMany(o => o.CargoAnnounceDetails)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);

        }

        private void BeforeSaveChanges()

        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            var originalValues = new Dictionary<string, object>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = _authenticatedUser.UserId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                originalValues[property.Metadata.Name] = entry.Property(property.Metadata.Name).OriginalValue;

                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }


    public class BlankTriggerAddingConvention : IModelFinalizingConvention
    {
        public virtual void ProcessModelFinalizing(
            IConventionModelBuilder modelBuilder,
            IConventionContext<IConventionModelBuilder> context)
        {
            foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
            {
                var table = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);
                if (table != null
                    && entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(table.Value) == null))
                {
                    entityType.Builder.HasTrigger(table.Value.Name + "Trigger");
                }

                foreach (var fragment in entityType.GetMappingFragments(StoreObjectType.Table))
                {
                    if (entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(fragment.StoreObject) == null))
                    {
                        entityType.Builder.HasTrigger(fragment.StoreObject.Name + "Trigger");
                    }
                }
            }
        }
    }
}