using System.Globalization;
using System.Text.RegularExpressions;
using AutoMapper;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.DTOs.Product;
using Sepehr.Application.Features.Customers.Command.CreateCustomer;
using Sepehr.Application.Features.Customers.Command.UpdateCustomer;
using Sepehr.Application.Features.Customers.Queries.GetAllCustomers;
using Sepehr.Application.Features.Orders.Command.CreateOrder;
using Sepehr.Application.Features.Orders.Command.UpdateOrder;
using Sepehr.Application.Features.Orders.Queries.GetAllOrders;
using Sepehr.Application.Features.Products.Command.CreateProduct;
using Sepehr.Application.Features.Products.Command.UpdateProduct;
using Sepehr.Application.Features.Products.Queries.GetAllProducts;
using Sepehr.Application.Features.ProductSuppliers.Command.CreateProductSupplier;
using Sepehr.Application.Features.ProductSuppliers.Command.UpdateProductSupplier;
using Sepehr.Application.Features.ProductSuppliers.Queries.GetAllProductSuppliers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;
using Sepehr.Domain;
using Sepehr.Application.Features.CargoAnnouncements.Command.CreateCargoAnnouncement;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllCargoAnncs;
using Sepehr.Application.Features.CargoAnnouncements.Command.UpdateCargoAnnouncement;
using Sepehr.Application.Features.ProductSuppliers.Queries.GetAllProducts;
using Sepehr.Application.Features.ReceivePays.Command.CreateReceivePay;
using Sepehr.Application.Features.ReceivePays.Queries.GetAllReceivePays;
using Sepehr.Application.Features.ReceivePays.Command.UpdateReceivePay;
using Microsoft.AspNetCore.Http;
using Sepehr.Application.Features.ProductPrices.Command.CreateProductPrice;
using Sepehr.Application.Features.ProductPrices.Command.UpdateProductPrice;
using Sepehr.Application.Features.ProductPrices.Queries.GetAllProductPrices;
using Sepehr.Application.Features.ReceivePays.Command.AccApproveReceivePay;
using Sepehr.Application.Features.Orders.Command.ConfirmOrder;
using Sepehr.Application.Features.ProductTypes.Command.CreateProductType;
using Sepehr.Application.Features.ProductTypes.Command.UpdateProductType;
using Sepehr.Application.Features.ProductTypes.Queries.GetAllProductTypes;
using Sepehr.Application.Features.ProductStates.Command.CreateProductState;
using Sepehr.Application.Features.ProductStates.Command.UpdateProductState;
using Sepehr.Application.Features.ProductStates.Queries.GetAllProductStates;
using Sepehr.Application.Features.ProductStandards.Command.CreateProductStandard;
using Sepehr.Application.Features.ProductStandards.Command.UpdateProductStandard;
using Sepehr.Application.Features.ProductStandards.Queries.GetAllProductStandards;
using Sepehr.Application.Features.Products.Command.DeleteProductById;
using Sepehr.Application.Features.Brands.Command.CreateBrand;
using Sepehr.Application.Features.Brands.Command.UpdateBrand;
using Sepehr.Application.Features.Brands.Queries.GetAllBrands;
using Sepehr.Application.Features.ProductBrands.Command.UpdateProductBrand;
using Sepehr.Application.Features.ProductBrands.Command.CreateProductBrand;
using Sepehr.Application.Features.ProductBrands.Queries.GetAllProductBrands;
using Sepehr.Domain.Enums;
using Sepehr.Application.Features.Services.Command.CreateService;
using Sepehr.Application.Features.Services.Queries.GetAllServices;
using Sepehr.Application.Features.Services.Command.UpdateService;
using Sepehr.Application.Features.Orders.Command.ApproveInvoiceType;
using Sepehr.Application.DTOs;
using Sepehr.Application.DTOs.LadingLicense;
using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;
using Sepehr.Application.Features.PurchaseOrders.Command.CreatePurchaseOrder;
using Sepehr.Application.Features.PurchaseOrders.Command.UpdatePurchaseOrder;
using Sepehr.Application.Features.Warehouses.Command.CreateWarehouse;
using Sepehr.Application.Features.Warehouses.Command.UpdateWarehouse;
using Sepehr.Application.Features.Warehouses.Queries.GetAllWarehouses;
using Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder;
using Sepehr.Application.DTOs.CustomerWarehouse;
using Sepehr.Application.Features.TransferRemittances.Command.CreateTransferRemittance;
using Sepehr.Application.Features.TransferRemittances.Command.UpdateTransferRemittance;
using Sepehr.Application.Features.TransferRemittances.Queries.GetAllTransferRemittances;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceEntrancePermission;
using Sepehr.Application.DTOs.TransferRemittanceUnloadingPermit;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceUnloadingPermit;
using Sepehr.Application.Features.ShareHolders.Command.CreateShareHolder;
using Sepehr.Application.Features.ShareHolders.Command.UpdateShareHolder;
using Sepehr.Application.Features.ShareHolders.Queries.GetAllShareHolders;
using Sepehr.Application.Features.Costs.Command.CreateCost;
using Sepehr.Application.Features.Costs.Command.UpdateCost;
using Sepehr.Application.Features.Costs.Queries.GetAllCosts;
using Sepehr.Application.Features.Incomes.Command.CreateIncome;
using Sepehr.Application.Features.Incomes.Command.UpdateIncome;
using Sepehr.Application.Features.OrganizationBanks.Command.CreateOrganizationBank;
using Sepehr.Application.Features.OrganizationBanks.Command.UpdateOrganizationBank;
using Sepehr.Application.Features.OrganizationBanks.Queries.GetAllOrganizationBanks;
using Sepehr.Application.Features.CashDesks.Command.UpdateCashDesk;
using Sepehr.Application.Features.CashDesks.Command.CreateCashDesk;
using Sepehr.Application.Features.PettyCashs.Command.CreatePettyCash;
using Sepehr.Application.Features.PettyCashs.Command.UpdatePettyCash;
using Sepehr.Application.Features.PettyCashs.Queries.GetAllPettyCashs;
using Sepehr.Application.Features.CashDesks.Queries.GetAllCashDesks;
using Sepehr.Application.Features.Incomes.Queries.GetAllIncomes;
using Sepehr.Application.Features.RentPayments.Command.CreateRentPayment;
using Sepehr.Application.Features.RentPayments.Command.UpdateRentPayment;
using Sepehr.Application.Features.LadingExitPermits.Command.UpdateLadingExitPermit;
using Sepehr.Application.Features.LadingExitPermits.Queries.GetAllLadingExitPermits;
using Sepehr.Application.Features.RentPayments.Queries.GetAllRentPayments;
using Sepehr.Application.Features.LadingExitPermits.Command.CreateLadingExitPermit;
using Sepehr.Application.DTOs.RentPayment;
using Sepehr.Application.Features.LadingPermits.Command.CreateLadingPermit;
using Sepehr.Application.Features.LadingPermits.Queries.GetAllLadingPermits;
using Sepehr.Application.Features.LadingPermits.Command.UpdateLadingPermit;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllNotSendedOrders;
using Sepehr.Application.DTOs.CargoAnnounce;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.DTOs.Users;
using Sepehr.Application.Features.UserRoles.Command.CreateUserRole;
using Sepehr.Application.DTOs.ApplicationRole;
using Sepehr.Application.DTOs.RoleMenu;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Application.Features.RolePermissions.Command.CreateRolePermission;
using Sepehr.Application.Features.RolePermissions.Queries.GetAllRolePermissions;
using Sepehr.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers;
using Sepehr.Application.Features.UserRoles.Queries.GetAllUserRoles;
using Sepehr.Application.Features.Permissions.Queries.GetAllPermissions;
using Sepehr.Application.Features.ApplicationUsers.Command.UpdateApplicationUser;
using Sepehr.Application.Features.ApplicationUsers.Command.CreateApplicationUser;
using Sepehr.Application.Features.ApplicationRoles.Command.UpdateApplicationRole;
using Sepehr.Application.Features.ApplicationRoles.Command.CreateApplicationRole;
using Sepehr.Application.Features.ApplicationRoles.Queries.GetAllApplicationRoles;
using Sepehr.Application.Features.Permissions.Command.CreatePermission;
using Sepehr.Application.Features.Permissions.Command.UpdatePermission;
using Sepehr.Application.Features.RolePermissions.Command.UpdateRolePermission;
using Sepehr.Application.Features.Permissions.Queries.GetAllPermissionsByMenu;
using Sepehr.Application.Features.LadingExitPermits.Command.RevokeLadingExitPermit;
using Sepehr.Application.Features.DriverFareAmounts;
using Sepehr.Application.Features.EntrancePermits.Command.CreateEntrancePermit;
using Sepehr.Application.Features.EntrancePermits.Queries.GetAllEntrancePermits;
using Sepehr.Application.Features.CustomerLabels.Command.CreateCustomerLabel;
using Sepehr.Application.Features.CustomerLabels.Command.UpdateCustomerLabel;
using Sepehr.Application.Features.CustomerLabels.Queries.GetAllCustomerLabels;
using Sepehr.Application.Features.Customers.Command.AssignCustomerLabel;
using Sepehr.Application.DTOs.Customer;
using Sepehr.Application.Features.UnloadingPermits.Queries.GetAllUnloadingPermits;
using Sepehr.Application.Features.CustomerOfficialCompanys.Command.CreateCustomerOfficialCompany;
using Sepehr.Application.Features.CustomerOfficialCompanys.Command.UpdateCustomerOfficialCompany;
using Sepehr.Application.Features.CustomerOfficialCompanys.Queries.GetAllCustomerOfficialCompanys;
using Sepehr.Application.Features.Personnels.Command.CreatePersonnel;
using Sepehr.Application.Features.Personnels.Command.UpdatePersonnel;
using Sepehr.Application.Features.Personnels.Queries.GetAllPersonnels;
using Sepehr.Application.Features.PaymentRequests.Command.CreatePaymentRequest;
using Sepehr.Application.Features.PaymentRequests.Command.UpdatePaymentRequest;
using Sepehr.Application.Features.PaymentRequests.Queries.GetAllPaymentRequests;
using Sepehr.Application.Features.TransferWarehouseInventories.Command.CreateTransferWarehouseInventory;
using Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetAllTransferWarehouseInventories;
using Sepehr.Application.Features.TransferWarehouseInventories.Command.UpdateTransferWarehouseInventory;
using Sepehr.Application.DTOs.TransferWarehouseInventory;
using Sepehr.Application.Features.PaymentRequests.Command.ApprovePaymentRequest;
using Sepehr.Application.Features.PaymentRequests.Command.RejectPaymentRequest;
using Sepehr.Application.Features.PaymentRequests.Command.ProceedPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.ApprovePersonnelPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.RejectPersonnelPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.ProceedPersonnelPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.CreatePersonnelPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.UpdatePersonnelPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Queries.GetAllPersonnelPaymentRequests;

namespace Sepehr.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<DapperProduct, ProductViewModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(d => d.ProductId))
                .ForMember(m => m.ProductStandardDesc, opt => opt.MapFrom(d => d.ProductStandardDesc))
                .ForMember(m => m.ProductStateDesc, opt => opt.MapFrom(d => d.ProductStateDesc))
                .ForMember(m => m.Inventory, opt => opt.MapFrom(d => d.ApproximateInventory))
                .ForMember(m => m.ProductIntegratedName, d => d.MapFrom(d =>
                    string.Concat(d.ProductName, ' ',
                        d.ProductSize, ' ', d.ProductStandardDesc, ' ', d.ProductStateDesc)))
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()));

            #region Product Inventory

            CreateMap<ProductInventory, ProductInventoryViewModel>()
                .ForMember(m => m.WarehouseName, d => d.MapFrom(d => d.Warehouse.Name))
                .ForMember(m => m.WarehouseType, d => d.MapFrom(d => d.Warehouse.WarehouseType.Description));
            CreateMap<ProductInventory, ProductInventoryDto>()
                .ForMember(m => m.WarehouseType, d => d.MapFrom(d => d.Warehouse.WarehouseType.Description));
            CreateMap<CreateProductInventoryCommand, ProductInventory>()
                .ForMember(m => m.ApproximateInventory, opt => opt.MapFrom(d => d.Inventory));
            #endregion

            #region Transfer Remittance
            CreateMap<GetAllTransferRemittancesQuery, GetAllTransferRemittancesParameter>().ReverseMap();
            CreateMap<CreateTransferRemittanceCommand, TransferRemittance>()
                    .ForMember(m => m.DeliverDate, opt => opt.MapFrom(d => d.DeliverDate.ToDateTime("00:00")));
            CreateMap<UpdateTransferRemittanceCommand, TransferRemittance>()
                    .ForMember(m => m.DeliverDate, opt => opt.MapFrom(d => d.DeliverDate.ToDateTime("00:00")));
            CreateMap<TransferRemittanceDetailDto, TransferRemittanceDetail>();
            CreateMap<TransferRemittance, EntrancePermit>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<TransferRemittanceDetail, TransferRemittanceDetailViewModel>()
                .ForMember(m => m.UnloadedAmount, opt => opt.MapFrom(d => d.UnloadingPermitDetail.Sum(u => u.UnloadedAmount)))
                .ForMember(m => m.Product, opt => opt.MapFrom(d => d.ProductBrand.Product))
                .ForMember(m => m.ProductName, opt => opt.MapFrom(d => d.ProductBrand.Product.ProductName))
                .ForMember(m => m.BrandName, opt => opt.MapFrom(d => d.ProductBrand.Brand.Name));


            CreateMap<EntrancePermit, EntrancePermitViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(m => m.UnloadingPermits, opt => opt.MapFrom(d => d.UnloadingPermits));

            CreateMap<TransferRemittance, TransferRemittanceViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(m => m.DestinationWarehouseName, opt => opt.MapFrom(d => d.DestinationWarehouse.Name))
                .ForMember(m => m.EntrancePermit, opt => opt.MapFrom(d => d.EntrancePermit))
                .ForMember(m => m.EntrancePermitId, opt => opt.MapFrom(d => d.EntrancePermit.Id))
                .ForMember(m => m.EntrancePermitCode, opt => opt.MapFrom(d => d.EntrancePermit.PermitCode))
                .ForMember(m => m.EntrancePermitDate, opt => opt.MapFrom(d => d.EntrancePermit == null ? "" :
                            d.EntrancePermit.Created.ToShamsiDate()))
                .ForMember(m => m.OriginWarehouseName, opt => opt.MapFrom(d => d.OriginWarehouse.Name))
                .ForMember(m => m.TransferRemittanceStatusDesc, opt => opt.MapFrom(d => d.TransferRemittanceStatus.StatusDesc))
                .ForMember(m => m.DeliverDate, opt => opt.MapFrom(d => d.DeliverDate.ToShamsiDate()))
                .ForMember(m => m.RegisterDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()))
                .ForMember(m => m.TransferRemittanceTypeDesc, opt => opt.MapFrom(d => d.TransferRemittanceType.RemittanceTypeDesc))
                .ForMember(m => m.VehicleTypeName, opt => opt.MapFrom(d => d.VehicleType.Name));

            #endregion

            #region دفترچه تلفن مشتری
            CreateMap<CreatePhonebookRequest, Phonebook>();
            CreateMap<Phonebook, PhonebookViewModel>()
                .ForMember(m => m.PhoneNumberTypeDesc, opt => opt.MapFrom(d => d.PhoneNumberType.TypeDescription));
            #endregion

            #region Products
            CreateMap<Product, OfficialWarehoseInventory>()
                .ForMember(m => m.ProductId, opt => opt.MapFrom(d => d.Id))
                .ForMember(m => m.IsActive, opt => opt.MapFrom(d => true))
                .ForMember(m => m.Id, opt => opt.Ignore());

            CreateMap<ProductDetail, ProductDetailViewModel>();

            CreateMap<Product, ProductViewModel>()
                .ForMember(m => m.CreatedDate, d => d.MapFrom(d => d.Created.ToShamsiDate()))
                .ForMember(m => m.ProductTypeDesc, d => d.MapFrom(d => d.ProductType.Desc))
                .ForMember(m => m.ProductPrices, d => d.MapFrom(d => d.ProductPrices))
                .ForMember(m => m.ProductPrice, d => d.MapFrom(d => d.ProductPrices.LastOrDefault().Price))
                .ForMember(m => m.ProductStateDesc, d => d.MapFrom(d => d.ProductState.Desc))
                .ForMember(m => m.ProductStandardDesc, d => d.MapFrom(d => d.ProductStandard.Desc))
                .ForMember(m => m.ProductMainUnitDesc, d => d.MapFrom(d => d.ProductMainUnit.UnitName))
                .ForMember(m => m.ProductSubUnitDesc, d => d.MapFrom(d => d.ProductSubUnit.UnitName))
                .ForMember(m => m.ProductTypeDesc, d => d.MapFrom(d => d.ProductType.Desc))
                .ForMember(m => m.ProductSubUnitDesc, d => d.MapFrom(d => d.ProductSubUnit.UnitName))
                .ForMember(m => m.ProductInventories, d => d.MapFrom(d => d.ProductInventories));

            CreateMap<CreateProductCommand, Product>()
                .ForMember(m => m.ProductSubUnitId, opt => opt.MapFrom(d => d.ProductSubUnitId == 0 ? null : d.ProductSubUnitId));
            CreateMap<EnableProductByIdCommand, Product>()
                .ForMember(m => m.ProductTypeId, opt => opt.Ignore())
                .ForMember(m => m.ProductName, opt => opt.Ignore())
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore());

            //.ForPath(m => m.ProductDetail.Standard, d => d.MapFrom(d => d.Standard))
            //.ForPath(m => m.ProductDetail.Size, d => d.MapFrom(d => d.Size))
            //.ForPath(m => m.ProductDetail.ProductState, d => d.MapFrom(d => d.ProductState));

            CreateMap<UpdateProductCommand, Product>()
                .ForMember(m => m.ProductTypeId, opt => opt.Ignore())
                .ForMember(m => m.ProductName, opt => opt.Ignore())
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore());

            CreateMap<DeleteProductByIdCommand, Product>()
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore());

            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
            CreateMap<GetAllProductsByTypeQuery, GetAllProductsParameter>();
            CreateMap<GetAllCustomersQuery, GetAllCustomersParameter>();
            CreateMap<ProductType, ProductTypeViewModel>();

            #endregion

            #region ProductPrices
            CreateMap<ProductPrice, ProductPriceViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(p => p.ProductId, opt => opt.MapFrom(b => b.ProductBrand.Product.Id))
                //.ForMember(p => p.ProductCode, opt => opt.MapFrom(b => b.ProductBrand.Product.ProductCode))
                .ForMember(p => p.ProductName, opt => opt.MapFrom(b => b.ProductBrand.Product.ProductName))
                .ForMember(p => p.RegisterDate, opt => opt.MapFrom(b => b.Created.ToShamsiDate()))
                .ForMember(p => p.BrandName, opt => opt.MapFrom(b => b.ProductBrand.Brand.Name));

            CreateMap<CreateProductPriceCommand, ProductPrice>();

            CreateMap<UpdateProductPriceCommand, ProductPrice>();

            CreateMap<GetAllProductPricesQuery, GetAllProductPricesParameter>();
            CreateMap<ExportAllProductPricesToExcelQuery, GetAllProductPricesParameter>();
            CreateMap<GetAllCustomersQuery, GetAllCustomersParameter>();

            #endregion

            #region CustomerOfficialCompany
            CreateMap<CustomerOfficialCompany, CustomerOfficialCompanyViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(p => p.CreatedDate, opt => opt.MapFrom(b => b.Created.ToShamsiDate()));

            CreateMap<CreateCustomerOfficialCompanyCommand, CustomerOfficialCompany>();

            CreateMap<UpdateCustomerOfficialCompanyCommand, CustomerOfficialCompany>();

            CreateMap<GetAllCustomerOfficialCompanysQuery, GetAllCustomerOfficialCompanysParameter>();
            CreateMap<GetAllCustomersQuery, GetAllCustomersParameter>();

            #endregion

            #region Product Types
            CreateMap<GetAllProductTypesQuery, GetAllProductTypesParameter>();
            CreateMap<ProductType, ProductTypeViewModel>();

            CreateMap<CreateProductTypeCommand, ProductType>();
            //.ForMember(m=>m.Image,opt=>opt.MapFrom(d=>Convert.ToByte(d.Image)));

            CreateMap<UpdateProductTypeCommand, ProductType>();

            CreateMap<GetAllLadingPermitsQuery, GetAllProductTypesParameter>();
            CreateMap<GetAllCustomersQuery, GetAllCustomersParameter>();

            #endregion

            #region Brands
            CreateMap<Brand, BrandViewModel>();

            CreateMap<CreateBrandCommand, Brand>();

            CreateMap<UpdateBrandCommand, Brand>();

            CreateMap<GetAllBrandsQuery, GetAllBrandsParameter>();

            #endregion

            #region ProductBrands

            CreateMap<ProductBrand, ProductBrandViewModel>()
                .ForMember(m => m.ProductName, opt => opt.MapFrom(d => d.Product.ProductName))
                .ForMember(m => m.BrandName, opt => opt.MapFrom(d => d.Brand.Name))
                .ForMember(m => m.ProductPrice, opt => opt.MapFrom(d => d.ProductPrices == null ? 0 : d.ProductPrices.FirstOrDefault().Price));

            CreateMap<CreateProductBrandCommand, ProductBrand>();

            CreateMap<UpdateProductBrandCommand, ProductBrand>();

            CreateMap<GetAllProductBrandsQuery, GetAllProductBrandsParameter>();

            CreateMap<ProductBrand, ProductInventory>()
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.ProductBrandId, opt => opt.MapFrom(d => d.Id))
                .ForMember(m => m.IsActive, opt => opt.MapFrom(d => true));

            #endregion

            #region Order Detail
            CreateMap<OrderDetailRequest, OrderDetail>()
                .ForMember(m => m.Id, d => d.MapFrom(d => d.Id))
                .ForMember(m => m.PurchaseSettlementDate, d => d.MapFrom(d => d.PurchaseSettlementDate.ToDateTime("00:00")));

            CreateMap<OrderDetail, OrderDetailViewModel>()
                .ForMember(m => m.ProductSubUnitDesc, d => d.MapFrom(d => d.ProductSubUnit.UnitName))
                .ForMember(m => m.WarehouseTypeId, d => d.MapFrom(d => d.Warehouse.WarehouseTypeId))
                .ForMember(m => m.WarehouseName, d => d.MapFrom(d => d.Warehouse.Name))
                .ForMember(m => m.AlternativeProductBrandName, d => d.MapFrom(d => d.AlternativeProductBrand == null ? "" : d.AlternativeProductBrand.Brand.Name))
                .ForMember(m => m.AlternativeProductName, d => d.MapFrom(d => d.AlternativeProductBrand == null ? "" : d.AlternativeProductBrand.Product.ProductName))
                .ForMember(m => m.BrandName, d => d.MapFrom(d => d.ProductBrand.Brand.Name))
                .ForMember(m => m.BrandId, d => d.MapFrom(d => d.ProductBrand.Brand.Id))
                .ForMember(m => m.ProductName, d => d.MapFrom(d => d.Product.ProductName))
                .ForMember(m => m.PurchaserCustomerName, d => d.MapFrom(d => string.Concat(d.PurchaserCustomer == null ? "" : d.PurchaserCustomer.FirstName, " ", d.PurchaserCustomer.LastName)))
                .ForMember(m => m.PurchaseInvoiceTypeId, d => d.MapFrom(d => d.PurchaseInvoiceType == null ? 0 : d.PurchaseInvoiceType.Id))
                .ForMember(m => m.PurchaseInvoiceTypeDesc, d => d.MapFrom(d => d.PurchaseInvoiceType == null ? "" : d.PurchaseInvoiceType.Desc))
                .ForMember(m => m.TotalLoadedAmount, d => d.MapFrom(d => d.CargoAnnounces == null ? 0 : d.CargoAnnounces.Sum(c => c.LadingAmount)))
                .ForMember(m => m.RemainingLadingAmount, d => d.MapFrom(d => d.CargoAnnounces == null ? d.ProximateAmount :
                                                         d.ProximateAmount - d.CargoAnnounces.Sum(c => c.LadingAmount)))
                .ForMember(m => m.RemainingAmountToLadingLicence, d =>
                        d.MapFrom(d => d.CargoAnnounces == null ? d.ProximateAmount :
                                    d.ProximateAmount - d.CargoAnnounces.Sum(l => l.LadingExitPermitDetail.RealAmount)))

                .ForMember(m => m.PurchaseSettlementDate, d => d.MapFrom(d => (d.PurchaseSettlementDate ?? DateTime.Now).ToShamsiDate())).ReverseMap();

            CreateMap<ApproveInvoiceOrderDetail, OrderDetail>();
            //.ForMember(m => m.AlternativeProductAmount, opt => opt.MapFrom(d=>d.alter)).MaxDepth(1);
            //.ForMember(m => m.Price, opt => opt.Ignore()).MaxDepth(1);

            #endregion

            #region Order
            CreateMap<OrderPaymentDto, OrderPayment>()
                .ForMember(m => m.PaymentDate, d => d.MapFrom(d => d.PaymentDate.ToDateTime("00:00")));

            CreateMap<OrderPayment, OrderPaymentViewModel>()
                .ForMember(m => m.PaymentDate, d => d.MapFrom(d => d.PaymentDate.ToShamsiDate()));

            CreateMap<CreateOrderCommand, Order>()
                .ForMember(m => m.DeliverDate, d => d.MapFrom(d => d.DeliverDate.ToDateTime("00:00")))
                .ForMember(m => m.FarePaymentTypeId, opt => opt.MapFrom(d => d.PaymentTypeId));
            //.ForMember(m => m.OrderTypeId, opt => opt.MapFrom(d => 
            //DateTime.Compare(d.DeliverDate, DateTime.Now.AddDays(3))<=0 ? OrderType.Urgant:OrderType.PreSale))
            //.ForMember(m => m.SettlementDate, d => d.MapFrom(d => d.SettlementDate.ToDateTime("00:00")));

            CreateMap<GetAllOrdersQuery, GetAllOrdersParameter>();

            CreateMap<CompleteAnnouncementCommand, Order>();

            CreateMap<Order, OrderViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName)))
                .ForMember(m => m.CustomerName, opt => opt.MapFrom(d => string.Concat(d.Customer.FirstName, " ", d.Customer.LastName)))
                .ForMember(m => m.RegisterDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()))
                .ForMember(m => m.DeliverDate, opt => opt.MapFrom(d => d.DeliverDate.ToShamsiDate()))
                .ForMember(m => m.OfficialName, opt => opt.MapFrom(d => d.Customer.OfficialName))
                .ForMember(m => m.PaymentTypeDesc, opt => opt.MapFrom(d => d.FarePaymentType.Desc))
                .ForMember(m => m.OrderTypeDesc, opt => opt.MapFrom(d => d.OrderTypeId == OrderType.Urgant ? "فروش فوری" : "پیش فروش"))
                .ForMember(m => m.OrderExitTypeDesc, opt => opt.MapFrom(d => d.OrderExitType == null ? "" : d.OrderExitType.ExitTypeDesc))
                .ForMember(m => m.OrderStatusDesc, opt => opt.MapFrom(d => d.OrderStatus.StatusDesc))
                //.ForMember(m => m.SettlementDate, opt => opt.MapFrom(d => d.SettlementDate.ToShamsiDate()))
                .ForMember(m => m.CustomerFirstName, opt => opt.MapFrom(d => d.Customer.FirstName))
                .ForMember(m => m.CustomerLastName, opt => opt.MapFrom(d => d.Customer.LastName))
                .ForMember(m => m.InvoiceTypeDesc, opt => opt.MapFrom(d => d.InvoiceType.TypeDesc))
                .ForMember(m => m.OrderSendTypeDesc, opt => opt.MapFrom(d => d.OrderSendType.Description))
                .ForMember(m => m.OrderPayments, opt => opt.MapFrom(d => d.OrderPayments))
                .ForMember(m => m.TotalLoadedAmount, opt =>
                                opt.MapFrom(d => d.CargoAnnounces
                                .Sum(x => x.CargoAnnounceDetails.Count() == 0 ? 0 : x.CargoAnnounceDetails.Sum(d => d.LadingAmount))))
                .ForMember(m => m.RemainingLadingAmount, opt =>
                                opt.MapFrom(d => d.CargoAnnounces
                                .Sum(x => x.CargoAnnounceDetails.Count() == 0 ? d.Details.Sum(o => o.ProximateAmount) :
                                d.Details.Sum(o => o.ProximateAmount) - x.CargoAnnounceDetails.Sum(d => d.LadingAmount))))

                .ForMember(m => m.Details, opt => opt.MapFrom(d => d.Details));

            CreateMap<UpdateOrderCommand, Order>()
                .ForMember(m => m.DeliverDate, d => d.MapFrom(d => d.DeliverDate.ToDateTime("00:00")))
                .ForMember(m => m.FarePaymentTypeId, opt => opt.MapFrom(d => d.PaymentTypeId))
                //.ForMember(m => m.OrderTypeId, opt => opt.MapFrom(d =>
                //            DateTime.Compare(d.DeliverDate, DateTime.Now.AddDays(3)) <= 0 ? OrderType.Urgant : OrderType.PreSale))
                .ForMember(m => m.OrderSendType, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.InvoiceType = null)
                .AfterMap((src, dest) => dest.OrderSendType = null)
                .AfterMap((src, dest) => dest.CustomerOfficialCompany = null)
                .AfterMap((src, dest) => dest.OrderExitType = null)
                .AfterMap((src, dest) => dest.Customer = null)
                .AfterMap((src, dest) => dest.FarePaymentType = null)
                .AfterMap((src, dest) => dest.OrderStatus = null)
                .ForMember(m => m.InvoiceType, opt => opt.Ignore())
                .ForMember(m => m.OrderCode, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore())
                .ForMember(m => m.Details, opt => opt.MapFrom(d => d.Details))
                .ForMember(m => m.Created, opt => opt.Ignore());

            CreateMap<AttachmentDto, Attachment>()
                .ForMember(m => m.FileData, opt => opt.MapFrom(d => ConvertToByte(d.FileData)));

            CreateMap<ApproveInvoiceTypeCommand, Order>()
                .ForPath(m => m.Details, opt => opt.Ignore())
                .ForMember(m => m.OrderCode, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore())
                .ForMember(m => m.Created, opt => opt.Ignore());

            CreateMap<OrderService, OrderServiceViewModel>()
                .ForMember(m => m.ServiceDesc, opt => opt.MapFrom(s => s.Service.Description));

            CreateMap<OrderServiceDto, OrderService>();


            #endregion

            #region Purchase Order
            CreateMap<OrderDetailRequest, PurchaseOrderDetail>()
                .ForMember(m => m.Id, d => d.MapFrom(d => d.Id));

            CreateMap<CreateOrderCommand, PurchaseOrder>()
                .ForMember(dest => dest.DestinationWarehouseId, opt => opt.MapFrom(src => "3"))
                .ForMember(dest => dest.OriginWarehouseId, opt => opt.MapFrom(src => "3"))
                .ForMember(m => m.OrderSendTypeId, opt => opt.MapFrom(d => d.OrderSendTypeId))
                .ForMember(m => m.FarePaymentTypeId, opt => opt.MapFrom(d => d.PaymentTypeId));

            //CreateMap<CreateOrderCommand, CreatePurchaseOrderCommand>()
            //    .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(d => d.Details.Sum(x => x.ProximateAmount * x.Price)))
            //    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(d => d.Details.First().PurchaserCustomerId))
            //    .ForMember(dest => dest.DestinationWarehouseId, opt => opt.MapFrom(src => "3"))
            //    .ForMember(dest => dest.OriginWarehouseId, opt => opt.MapFrom(src => "3"))
            //    .ForMember(m => m.PurchaseOrderSendTypeId, opt => opt.MapFrom(d => d.OrderSendTypeId))
            //    .ForMember(m => m.PaymentTypeId, opt => opt.MapFrom(d => d.PaymentTypeId));

            //CreateMap<UpdateOrderCommand, CreatePurchaseOrderCommand>()
            //    .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(d => d.Details.Sum(x => x.ProximateAmount * x.Price)))
            //    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(d => d.Details.First().PurchaserCustomerId))
            //    .ForMember(dest => dest.DestinationWarehouseId, opt => opt.MapFrom(src => "3"))
            //    .ForMember(dest => dest.OriginWarehouseId, opt => opt.MapFrom(src => "3"))
            //    .ForMember(m => m.PurchaseOrderSendTypeId, opt => opt.MapFrom(d => d.OrderSendTypeId))
            //    .ForMember(m => m.PaymentTypeId, opt => opt.MapFrom(d => d.PaymentTypeId));

            CreateMap<CreatePurchaseOrderDto, PurchaseOrder>()
                .ForMember(m => m.ExitType, opt => opt.MapFrom(d => ExitType.Usual))
                .ForMember(m => m.FarePaymentTypeId, opt => opt.MapFrom(d => 1))
                .ForMember(m => m.FarePaymentTypeId, opt => opt.MapFrom(d => 1))
                .ForMember(m => m.OrderSendTypeId, opt => opt.MapFrom(d => 1))
                .ForMember(dest => dest.DestinationWarehouseId, opt => opt.MapFrom(d => 3))
                .ForMember(dest => dest.OriginWarehouseId, opt => opt.MapFrom(d => 3));



            CreateMap<OrderDetailRequest, CreatePurchaseOrderDetailRequest>()
                .ForMember(m => m.ProductBrandId, opt => opt.MapFrom(d => d.ProductBrandId))
                .ForMember(m => m.ProximateAmount, opt => opt.MapFrom(d => d.ProximateAmount));

            CreateMap<OrderPaymentDto, PurchaseOrderPaymentDto>();
            //CreateMap<OrderServiceDto, PurchaseOrderServiceDto>();            

            CreateMap<OrderPaymentDto, PurchaseOrderPayment>()
                .ForMember(m => m.PaymentDate, d => d.MapFrom(d => d.PaymentDate.ToDateTime("00:00")));

            CreateMap<UpdatePurchaseOrderDetailRequest, PurchaseOrderDetail>()
                .ForMember(m => m.DeliverDate, d => d.MapFrom(d => d.DeliverDate.ToDateTime("00:00")))
                .ForMember(m => m.Id, d => d.MapFrom(d => d.Id));

            CreateMap<PurchaseOrderPaymentDto, PurchaseOrderPayment>()
                .ForMember(m => m.PaymentDate, d => d.MapFrom(d => d.PaymentDate.ToDateTime("00:00")));

            CreateMap<PurchaseOrderPayment, OrderPaymentViewModel>()
                .ForMember(m => m.PaymentDate, d => d.MapFrom(d => d.PaymentDate.ToShamsiDate()));

            CreateMap<CreatePurchaseOrderDetailRequest, PurchaseOrderDetail>()
                .ForMember(m => m.DeliverDate, d => d.MapFrom(d => d.DeliverDate.ToDateTime("00:00")));

            CreateMap<PurchaseOrderDetail, PurchaseOrderDetailViewModel>()
                .ForMember(m => m.ProductName, opt => opt.MapFrom(d => d.ProductBrand.Product.ProductName))
                .ForMember(m => m.DeliverDate, d => d.MapFrom(d => d.DeliverDate.ToShamsiDate()));


            CreateMap<CreatePurchaseOrderCommand, PurchaseOrder>()
                .ForMember(m => m.OriginWarehouseId, opt => opt.MapFrom(d => d.OriginWarehouseId))
                .ForMember(m => m.OrderSendTypeId, opt => opt.MapFrom(d => d.PurchaseOrderSendTypeId))
                .ForMember(m => m.FarePaymentTypeId, opt => opt.MapFrom(d => d.PaymentTypeId));

            CreateMap<GetAllPurchaseOrdersQuery, GetAllPurchaseOrdersParameter>();

            CreateMap<CompleteAnnouncementCommand, PurchaseOrder>();
            CreateMap<PurchaseOrder, PurchaseOrderViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName)))
                .ForMember(m => m.CustomerName, opt => opt.MapFrom(d => string.Concat(d.Customer.FirstName, " ", d.Customer.LastName)))
                .ForMember(m => m.OriginWarehouseDesc, opt => opt.MapFrom(d => d.OriginWarehouse.Name))
                .ForMember(m => m.DestinationWarehouseDesc, opt => opt.MapFrom(d => d.DestinationWarehouse.Name))
                .ForMember(m => m.RegisterDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()))
                //.ForMember(m => m.OrderExitTypeDesc, opt => opt.MapFrom(d => d.OrderExitType == null ? "" : d.OrderExitType.ExitTypeDesc))
                .ForMember(m => m.OfficialName, opt => opt.MapFrom(d => d.Customer.OfficialName))
                .ForMember(m => m.OrderStatusDesc, opt => opt.MapFrom(d => d.OrderStatus.StatusDesc))
                .ForMember(m => m.CustomerFirstName, opt => opt.MapFrom(d => d.Customer.FirstName))
                .ForMember(m => m.CustomerLastName, opt => opt.MapFrom(d => d.Customer.LastName))
                .ForMember(m => m.InvoiceTypeDesc, opt => opt.MapFrom(d => d.InvoiceType.TypeDesc))
                .ForMember(m => m.OrderSendTypeDesc, opt => opt.MapFrom(d => d.OrderSendType.SendTypeDesc))
                .ForMember(m => m.Details, opt => opt.MapFrom(d => d.Details))
                .ForMember(m => m.PaymentTypeDesc, opt => opt.MapFrom(d => d.FarePaymentType.TypeDesc));

            CreateMap<UpdatePurchaseOrderCommand, PurchaseOrder>()
                .ForMember(m => m.FarePaymentTypeId, opt => opt.MapFrom(d => d.PaymentTypeId))
                .ForMember(m => m.OrderSendTypeId, opt => opt.MapFrom(d => d.PurchaseOrderSendTypeId))
                .ForMember(m => m.OrderCode, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore())
                .ForMember(m => m.Details, opt => opt.MapFrom(d => d.Details))
                .ForMember(m => m.Created, opt => opt.Ignore());

            CreateMap<AttachmentDto, Attachment>()
                .ForMember(m => m.FileData, opt => opt.MapFrom(d => ConvertToByte(d.FileData)));

            CreateMap<ApproveInvoiceOrderDetail, PurchaseOrderDetail>();
            CreateMap<ApprovePurchaseOrderInvoiceTypeCommand, PurchaseOrder>()
                //.ForPath(m => m.OrderStatusId, opt => opt.Ignore())
                //.ForPath(m => m.Details, opt => opt.Ignore())
                .ForMember(m => m.OrderCode, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore())
                .ForMember(m => m.Created, opt => opt.Ignore());

            CreateMap<PurchaseOrderDetail, OfficialWarehoseInventory>()
                .ForMember(m => m.ProductId, opt => opt.MapFrom(d => d.ProductBrand.ProductId))
                .ForMember(m => m.WarehouseId, opt => opt.MapFrom(d => 2))
                .ForMember(m => m.ApproximateInventory, opt => opt.MapFrom(d => d.AlternativeProductAmount == 0 ? d.ProximateAmount : d.AlternativeProductAmount));



            CreateMap<PurchaseOrderService, OrderServiceViewModel>()
                .ForMember(m => m.ServiceDesc, opt => opt.MapFrom(s => s.Service.Description));

            CreateMap<OrderServiceDto, PurchaseOrderService>();


            #endregion

            #region Customers
            CreateMap<CustomerWarehouseDto, CustomerWarehouse>();
            CreateMap<CreateCustomerCommand, Customer>()
                .ForMember(m => m.Phonebook, opt => opt.MapFrom(d => d.Phonebook));
            CreateMap<CustomerWarehouse, WarehouseViewModel>()
                .ForMember(m => m.WarehouseTypeId, opt => opt.MapFrom(d => d.Warehouse.WarehouseTypeId))
                .ForMember(m => m.WarehouseTypeDesc, opt => opt.MapFrom(d => d.Warehouse.WarehouseType.Description))
                .ForMember(m => m.Name, opt => opt.MapFrom(d => d.Warehouse.Name))
                .ForMember(m => m.Id, opt => opt.MapFrom(d => d.Warehouse.Id));

            CreateMap<Customer, CustomerViewModel>()
                .ForMember(m => m.Warehouses, opt => opt.MapFrom(d => d.CustomerWarehouses))
                .ForMember(m => m.CustomerValidityColorCode, opt => opt.MapFrom(d => d.CustomerValidity.ColorCode))
                .ForMember(m => m.CustomerValidityDesc, opt => opt.MapFrom(d => d.CustomerValidity.ValidityDesc))
                .ForMember(m => m.CustomerDept, opt =>
                opt.MapFrom(d => d.Orders == null ? 0 :
                (
                    d.Orders//.Where(o=> o.OrderTypeId==OrderType.Sale)
                    .Sum(o => o.TotalAmount) -
                    //d.Orders.Where(o=>o.OrderTypeId== OrderType.Purchase).Sum(o=>o.TotalAmount) -
                    (d.ReceivePaymentSourceFrom == null ? 0 : d.ReceivePaymentSourceFrom.Sum(p => p.Amount)) +
                    (d.ReceivePaymentSourceTo == null ? 0 : d.ReceivePaymentSourceTo.Sum(p => p.Amount))
                )))
                .ForMember(m => m.CustomerCurrentDept, opt =>
                opt.MapFrom(d => d.Orders == null ? 0 :
                (
                    d.Orders.Where(o => o.Created.Date < DateTime.Now.Date).Sum(o => o.TotalAmount) -
                    //d.Orders.Where(o => o.OrderTypeId == OrderType.Purchase && o.Created.Date < DateTime.Now.Date).Sum(o => o.TotalAmount) -
                    (d.ReceivePaymentSourceFrom == null ? 0 : d.ReceivePaymentSourceFrom.Sum(p => p.Amount)) +
                    (d.ReceivePaymentSourceTo == null ? 0 : d.ReceivePaymentSourceTo.Sum(p => p.Amount))
                )))
                .ForMember(m => m.SettlementTypeDesc, opt =>
                opt.MapFrom(d => string.Format("{0} روز {1} خروج", (d.SettlementType == SettlementType.AfterExit ? "بعد" : "قبل"), d.SettlementDay)));

            CreateMap<UpdateCustomerCommand, Customer>()
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore());

            CreateMap<AssignCustomerLabelCommand, IEnumerable<CustomerAssignedLabelDto>>()
            .ConvertUsing(src => src.AssignedLabels.Select(labelId => new CustomerAssignedLabelDto
            {
                CustomerId = src.CustomerId,
                LabelId = labelId
            }));

            CreateMap<CustomerAssignedLabelDto, CustomerAssignedLabel>()
                .ForMember(m => m.CustomerLabelId, d => d.MapFrom(d => d.LabelId));

            CreateMap<CustomerAssignedLabel, CustomerAssignedLabelViewModel>()
                .ForMember(m => m.CustomerLabelName, opt => opt.MapFrom(d =>
                d.CustomerLabel.Product != null ? d.CustomerLabel.Product.ProductName :
                d.CustomerLabel.Brand != null ? d.CustomerLabel.Brand.Name :
                d.CustomerLabel.ProductType != null ? d.CustomerLabel.ProductType.Desc :
                d.CustomerLabel.ProductBrand != null ? string.Concat(d.CustomerLabel.ProductBrand.Product.ProductName, ' ', d.CustomerLabel.ProductBrand.Brand.Name) :
                d.CustomerLabel.LabelName
                ));
                //.ForMember(m => m.BrandName, d => d.MapFrom(d => d.CustomerLabel.Brand.Name))
                //.ForMember(m => m.ProductName, d => d.MapFrom(d => d.CustomerLabel.Product.ProductName))
                //.ForMember(m => m.ProductTypeName, d => d.MapFrom(d => d.CustomerLabel.ProductType.Desc))
                //.ForMember(m => m.CustomerLabelName, d => d.MapFrom(d => d.CustomerLabel.Brand.Name))


            #endregion

            #region ProductSuppliers
            CreateMap<CreateProductSupplierCommand, ProductSupplier>()
                .ForMember(m => m.PriceDate, opt => opt.MapFrom(o => o.PriceDate.ToDateTime("00:00")));
            CreateMap<GetAllProductSuppliersQuery, GetAllProductSuppliersParameter>();
            CreateMap<ProductSupplier, ProductSupplierViewModel>()
                .ForMember(m => m.CustomerFirstName, opt => opt.MapFrom(d => d.Customer.FirstName))
                .ForMember(m => m.CustomerLastName, opt => opt.MapFrom(d => d.Customer.LastName))
                .ForMember(m => m.ProductName, opt => opt.MapFrom(d => d.Product.ProductName))
                .ForMember(m => m.PriceDate, opt => opt.MapFrom(d => d.PriceDate.ToShamsiDate()));
            CreateMap<UpdateProductSupplierCommand, ProductSupplier>()
                .ForMember(m => m.PriceDate, opt => opt.MapFrom(o => o.PriceDate.ToDateTime("00:00")))
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore());
            #endregion

            #region ReceivePay
            CreateMap<AccRegisterReceivePayCommand, ReceivePay>();
            CreateMap<AccApproveReceivePayCommand, ReceivePay>();

            CreateMap<CreateReceivePayCommand, ReceivePay>()
                .ForMember(m => m.ReceiveFromCustomerId, opt =>
                    opt.MapFrom(d => (d.ReceivePaymentTypeFromId == 1 ? (Guid?)Guid.Parse(d.ReceiveFromId) : null)))
                .ForMember(m => m.ReceiveFromOrganizationBankId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeFromId == 2 ? (int?)int.Parse(d.ReceiveFromId) : null))
                .ForMember(m => m.ReceiveFromCashDeskId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeFromId == 3 ? (int?)int.Parse(d.ReceiveFromId) : null))
                .ForMember(m => m.ReceiveFromIncomeId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeFromId == 4 ? (int?)int.Parse(d.ReceiveFromId) : null))
                .ForMember(m => m.ReceiveFromPettyCashId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeFromId == 5 ? (int?)int.Parse(d.ReceiveFromId) : null))
                .ForMember(m => m.ReceiveFromCostId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeFromId == 6 ? (int?)int.Parse(d.ReceiveFromId) : null))
                .ForMember(m => m.ReceiveFromShareHolderId, opt =>
                    opt.MapFrom(d => new int[] { 7, 8 }.Contains((int)d.ReceivePaymentTypeFromId) ? (Guid?)Guid.Parse(d.ReceiveFromId) : null))

                .ForMember(m => m.PayToCustomerId, opt =>
                    opt.MapFrom(d => (d.ReceivePaymentTypeToId == 1 ? (Guid?)Guid.Parse(d.PayToId) : null)))
                .ForMember(m => m.PayToOrganizationBankId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeToId == 2 ? (int?)int.Parse(d.PayToId) : null))
                .ForMember(m => m.PayToCashDeskId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeToId == 3 ? (int?)int.Parse(d.PayToId) : null))
                .ForMember(m => m.PayToIncomeId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeToId == 4 ? (int?)int.Parse(d.PayToId) : null))
                .ForMember(m => m.PayToPettyCashId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeToId == 5 ? (int?)int.Parse(d.PayToId) : null))
                .ForMember(m => m.PayToCostId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeToId == 6 ? (int?)int.Parse(d.PayToId) : null))
                .ForMember(m => m.PayToShareHolderId, opt =>
                    opt.MapFrom(d => new int[] { 7, 8 }.Contains((int)d.ReceivePaymentTypeToId) ? (Guid?)Guid.Parse(d.PayToId) : null))

                .ForMember(m => m.Attachments, opt => opt.MapFrom(d => ConvertToByte(d.Attachments)));

            CreateMap<GetAllReceivePaysQuery, GetAllReceivePaysParameter>();
            CreateMap<Attachment, AttachmentViewModel>();
            CreateMap<ReceivePay, ReceivePayViewModel>()
                .ForMember(m => m.ReceivePaymentTypeFromDesc, opt => opt.MapFrom(d => d.ReceivePaymentTypeFrom.Desc))
                .ForMember(m => m.ReceivePaymentTypeToDesc, opt => opt.MapFrom(d => d.ReceivePaymentTypeTo.Desc))

                .ForMember(m => m.ReceiveFromId, opt => opt.MapFrom(d =>
                d.ReceivePaymentTypeFromId == 1 ? d.ReceiveFromCustomer.Id.ToString() :
                d.ReceivePaymentTypeFromId == 2 ? d.ReceiveFromOrganizationBank.Id.ToString() :
                d.ReceivePaymentTypeFromId == 3 ? d.ReceiveFromCashDesk.Id.ToString() :
                d.ReceivePaymentTypeFromId == 4 ? d.ReceiveFromIncome.Id.ToString() :
                d.ReceivePaymentTypeFromId == 5 ? d.ReceiveFromPettyCash.Id.ToString() :
                d.ReceivePaymentTypeFromId == 6 ? d.ReceiveFromCost.Id.ToString() :
                d.ReceivePaymentTypeFromId == 7 ? d.ReceiveFromShareHolder.Id.ToString() :
                d.ReceivePaymentTypeFromId == 8 ? d.ReceiveFromShareHolder.Id.ToString() :
                ""
                ))
                .ForMember(m => m.ReceiveFromDesc, opt => opt.MapFrom(d =>
                d.ReceivePaymentTypeFromId == 1 ? string.Concat(d.ReceiveFromCustomer.FirstName, " ", d.ReceiveFromCustomer.LastName) :
                d.ReceivePaymentTypeFromId == 2 ? d.ReceiveFromOrganizationBank.Bank.BankName :
                d.ReceivePaymentTypeFromId == 3 ? d.ReceiveFromCashDesk.CashDeskDescription :
                d.ReceivePaymentTypeFromId == 4 ? d.ReceiveFromIncome.IncomeDescription :
                d.ReceivePaymentTypeFromId == 5 ? d.ReceiveFromPettyCash.PettyCashDescription :
                d.ReceivePaymentTypeFromId == 6 ? d.ReceiveFromCost.CostDescription :
                d.ReceivePaymentTypeFromId == 7 ? string.Concat(d.ReceiveFromShareHolder.FirstName, " ", d.ReceiveFromShareHolder.LastName) :
                d.ReceivePaymentTypeFromId == 8 ? string.Concat(d.ReceiveFromShareHolder.FirstName, " ", d.ReceiveFromShareHolder.LastName) :
                ""
                ))

                .ForMember(m => m.PayToId, opt => opt.MapFrom(d =>
                d.ReceivePaymentTypeToId == 1 ? d.PayToCustomer.Id.ToString() :
                d.ReceivePaymentTypeToId == 2 ? d.PayToOrganizationBank.Id.ToString() :
                d.ReceivePaymentTypeToId == 3 ? d.PayToCashDesk.Id.ToString() :
                d.ReceivePaymentTypeToId == 4 ? d.PayToIncome.Id.ToString() :
                d.ReceivePaymentTypeToId == 5 ? d.PayToPettyCash.Id.ToString() :
                d.ReceivePaymentTypeToId == 6 ? d.PayToCost.Id.ToString() :
                d.ReceivePaymentTypeToId == 7 ? d.PayToShareHolder.Id.ToString() :
                d.ReceivePaymentTypeToId == 8 ? d.PayToShareHolder.Id.ToString() :
                ""
                ))
                .ForMember(m => m.PayToDesc, opt => opt.MapFrom(d =>
                d.ReceivePaymentTypeToId == 1 ? string.Concat(d.PayToCustomer.FirstName, " ", d.PayToCustomer.LastName) :
                d.ReceivePaymentTypeToId == 2 ? d.PayToOrganizationBank.Bank.BankName :
                d.ReceivePaymentTypeToId == 3 ? d.PayToCashDesk.CashDeskDescription :
                d.ReceivePaymentTypeToId == 4 ? d.PayToIncome.IncomeDescription :
                d.ReceivePaymentTypeToId == 5 ? d.PayToPettyCash.PettyCashDescription :
                d.ReceivePaymentTypeToId == 6 ? d.PayToCost.CostDescription :
                d.ReceivePaymentTypeToId == 7 ? string.Concat(d.PayToShareHolder.FirstName, " ", d.PayToShareHolder.LastName) :
                d.ReceivePaymentTypeToId == 8 ? string.Concat(d.PayToShareHolder.FirstName, " ", d.PayToShareHolder.LastName) :
                ""
                ))

                .ForMember(m => m.ReceiveFromCompanyName, opt => opt.MapFrom(d =>
                d.ReceivePaymentTypeFromId == 1 ? (d.ReceiveFromCompany == null ? "" : d.ReceiveFromCompany.CompanyName) : ""))

                .ForMember(m => m.PayToCompanyName, opt => opt.MapFrom(d =>
                d.ReceivePaymentTypeToId == 1 ? (d.PayToCompany == null ? "" : d.PayToCompany.CompanyName) : ""))

                .ForMember(m => m.ReceiveFromCompanyId, opt => opt.MapFrom(d =>
                d.ReceivePaymentTypeFromId == 1 ? (d.ReceiveFromCompany == null ? null : d.ReceiveFromCompanyId) : null))

                .ForMember(m => m.PayToCompanyId, opt => opt.MapFrom(d =>
                d.ReceivePaymentTypeFromId == 1 ? (d.PayToCompany == null ? null : d.PayToCompanyId) : null))


                .ForMember(m => m.ReceivePayStatusDesc, opt => opt.MapFrom(d => d.ReceivePayStatus.StatusDesc))
                .ForMember(m => m.AccountingApprovalDate, opt => opt.MapFrom(d => d.AccountingApprovalDate.ToShamsiDate()))
                .ForMember(m => m.Attachments, opt => opt.MapFrom(d => d.Attachments))
                .ForMember(m => m.HasAttachment, opt => opt.MapFrom(d => d.Attachments.Any()))

                .ForMember(m => m.Attachments, opt => opt.MapFrom(d => d.Attachments))
                .ForMember(m => m.AccountingApprovalDate, opt => opt.MapFrom(d => d.AccountingApprovalDate.ToShamsiDate()));

            CreateMap<UpdateReceivePayCommand, ReceivePay>()
                .ForMember(m => m.ReceiveFromCustomerId, opt =>
                    opt.MapFrom(d => (d.ReceivePaymentTypeFromId == 1 ? (Guid?)Guid.Parse(d.ReceiveFromId) : null)))
                .ForMember(m => m.ReceiveFromOrganizationBankId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeFromId == 2 ? (int?)int.Parse(d.ReceiveFromId) : null))
                .ForMember(m => m.ReceiveFromCashDeskId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeFromId == 3 ? (int?)int.Parse(d.ReceiveFromId) : null))
                .ForMember(m => m.ReceiveFromIncomeId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeFromId == 4 ? (int?)int.Parse(d.ReceiveFromId) : null))
                .ForMember(m => m.ReceiveFromPettyCashId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeFromId == 5 ? (int?)int.Parse(d.ReceiveFromId) : null))
                .ForMember(m => m.ReceiveFromCostId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeFromId == 6 ? (int?)int.Parse(d.ReceiveFromId) : null))
                .ForMember(m => m.ReceiveFromShareHolderId, opt =>
                    opt.MapFrom(d => new int[] { 7, 8 }.Contains((int)d.ReceivePaymentTypeFromId) ? (Guid?)Guid.Parse(d.ReceiveFromId) : null))

                .ForMember(m => m.PayToCustomerId, opt =>
                    opt.MapFrom(d => (d.ReceivePaymentTypeToId == 1 ? (Guid?)Guid.Parse(d.PayToId) : null)))
                .ForMember(m => m.PayToOrganizationBankId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeToId == 2 ? (int?)int.Parse(d.PayToId) : null))
                .ForMember(m => m.PayToCashDeskId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeToId == 3 ? (int?)int.Parse(d.PayToId) : null))
                .ForMember(m => m.PayToIncomeId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeToId == 4 ? (int?)int.Parse(d.PayToId) : null))
                .ForMember(m => m.PayToPettyCashId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeToId == 5 ? (int?)int.Parse(d.PayToId) : null))
                .ForMember(m => m.PayToCostId, opt =>
                    opt.MapFrom(d => d.ReceivePaymentTypeToId == 6 ? (int?)int.Parse(d.PayToId) : null))
                .ForMember(m => m.PayToShareHolderId, opt =>
                    opt.MapFrom(d => new int[] { 7, 8 }.Contains((int)d.ReceivePaymentTypeToId) ? (Guid?)Guid.Parse(d.PayToId) : null))

                .ForMember(m => m.Attachments, opt => opt.MapFrom(d => d.Attachments == null ? null : ConvertToByte(d.Attachments)));
            #endregion

            #region ladingPermit
            CreateMap<CreateLadingPermitCommand, LadingPermit>();

            CreateMap<GetAllLadingPermitsQuery, GetAllLadingPermitsParameter>();
            CreateMap<LadingPermit, LadingPermitViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()));

            CreateMap<UpdateLadingPermitCommand, LadingLicense>()
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore());

            #endregion

            #region CargoAnnouncement
            CreateMap<CreateCargoAnncCommand, CargoAnnounce>()
                .ForMember(m => m.DriverMobile, opt => opt.MapFrom(d => d.DriverMobile.ToEnNumber()))
                .ForMember(m => m.DeliveryDate, opt => opt.MapFrom(d => d.DeliveryDate.ToDateTime("00:00")));
            //.ForMember(m => m.ApprovedDate, opt => opt.MapFrom(d => d.ApprovedDate.ToDateTime("00:00")));
            CreateMap<RevokeCargoAnncCommand, CargoAnnounce>();
            CreateMap<CargoAnnounceDetailDto, CargoAnnounceDetail>();
            CreateMap<GetAllCargoAnncsQuery, GetAllCargoAnncsParameter>();
            CreateMap<CargoAnnounceDetail, CargoAnncDetailViewModel>();
            CreateMap<CargoAnnounce, CargoAnncViewModel>()
                .ForMember(m => m.CreatedBy, opt => opt.MapFrom(d => string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName)))
                .ForMember(m => m.VehicleTypeName, opt => opt.MapFrom(d => d.VehicleType == null ? "" : d.VehicleType.Name))
                .ForMember(m => m.DeliveryDate, opt => opt.MapFrom(d => d.DeliveryDate.ToShamsiDate()))
                .ForMember(m => m.CargoAnnounceDetails, opt => opt.MapFrom(d => d.CargoAnnounceDetails))
                .ForMember(m => m.ApprovedDate, opt => opt.MapFrom(d => d.ApprovedDate.ToShamsiDate()));

            CreateMap<UpdateCargoAnncCommand, CargoAnnounce>()
                .ForMember(m => m.DeliveryDate, opt => opt.MapFrom(d => d.DeliveryDate.ToDateTime("00:00")))
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore());

            CreateMap<GetAllNotSendedOrdersQuery, GetNotAnnouncedOrdersParameter>();
            CreateMap<Order, CargoAnncOrderViewModel>()
                .ForMember(m => m.CustomerName, opt => opt.MapFrom(d => string.Concat(d.Customer.FirstName, " ", d.Customer.LastName)))
                .ForMember(m => m.RegisterDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()))
                .ForMember(m => m.DeliverDate, opt => opt.MapFrom(d => d.DeliverDate.ToShamsiDate()))
                .ForMember(m => m.OfficialName, opt => opt.MapFrom(d => d.Customer.OfficialName))
                .ForMember(m => m.PaymentTypeDesc, opt => opt.MapFrom(d => d.FarePaymentType.Desc))
                .ForMember(m => m.OrderTypeDesc, opt => opt.MapFrom(d => d.OrderTypeId == OrderType.Urgant ? "فروش فوری" : "پیش فروش"))
                .ForMember(m => m.OrderStatusDesc, opt => opt.MapFrom(d => d.OrderStatus.StatusDesc))
                .ForMember(m => m.CustomerFirstName, opt => opt.MapFrom(d => d.Customer.FirstName))
                .ForMember(m => m.CustomerLastName, opt => opt.MapFrom(d => d.Customer.LastName))
                .ForMember(m => m.InvoiceTypeDesc, opt => opt.MapFrom(d => d.InvoiceType.TypeDesc))
                .ForMember(m => m.OrderSendTypeDesc, opt => opt.MapFrom(d => d.OrderSendType.Description));
            #endregion

            #region Service
            CreateMap<CreateServiceCommand, Service>();
            CreateMap<GetAllServicesQuery, GetAllServicesParameter>();
            CreateMap<Service, ServiceViewModel>();
            CreateMap<UpdateServiceCommand, Service>();
            #endregion

            #region Product States
            CreateMap<ProductState, ProductStateViewModel>();

            CreateMap<CreateProductStateCommand, ProductState>();

            CreateMap<UpdateProductStateCommand, ProductState>();

            CreateMap<GetAllProductStatesQuery, GetAllProductStatesParameter>();
            CreateMap<GetAllCustomersQuery, GetAllCustomersParameter>();

            #endregion

            #region Product Standards
            CreateMap<ProductStandard, ProductStandardViewModel>();

            CreateMap<CreateProductStandardCommand, ProductStandard>();

            CreateMap<UpdateProductStandardCommand, ProductStandard>();

            CreateMap<GetAllProductStandardsQuery, GetAllProductStandardsParameter>();
            CreateMap<GetAllCustomersQuery, GetAllCustomersParameter>();

            #endregion

            #region Warehouses
            CreateMap<Warehouse, WarehouseViewModel>()
                //.ForMember(m => m.CustomerName, opt => opt.MapFrom(d => d.Customer==null ? "": string.Concat(d.Customer.FirstName, " ",d.Customer.LastName)))
                .ForMember(m => m.WarehouseTypeDesc, opt => opt.MapFrom(d => d.WarehouseType.Description));
            CreateMap<CreateWarehouseCommand, Warehouse>();

            CreateMap<UpdateWarehouseCommand, Warehouse>();

            CreateMap<GetAllWarehousesQuery, GetAllWarehousesParameter>();
            #endregion

            #region Entrance Permit مجوز ورود حواله نقل و انتقال
            CreateMap<GetAllEntrancePermitsQuery, GetAllEntrancePermitsParameter>();
            CreateMap<CreateEntrancePermitCommand, EntrancePermit>();
            CreateMap<EntrancePermit, EntrancePermitViewModel>()
                .ForMember(m => m.TransferRemitance, opt => opt.MapFrom(d => d.TransferRemittance))
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()));

            #endregion

            #region Transfer Purchase Order
            CreateMap<TransferRemittanceEntrancePermissionCommand, EntrancePermit>();

            CreateMap<TransferRemittance, EntrancePermit>()
                .ForMember(m => m.TransferRemittanceId, opt => opt.MapFrom(d => d.Id));
            CreateMap<TransferPurchaseOrderCommand, PurchaseOrderTransfer>()
                .ForMember(m => m.PurchaseOrderId, opt => opt.MapFrom(d => d.OrderId))
                .ForMember(m => m.PurchaseOrderTransferDetails, opt => opt.MapFrom(d => d.TransferDetails));
            CreateMap<TransferPurchaseOrderDetailDto, PurchaseOrderTransferDetail>();

            CreateMap<UnloadingPermit, TransferRemittance>()
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore())
                .ForMember(m => m.Id, opt => opt.Ignore())
                .ForMember(m => m.DeliverDate, opt => opt.MapFrom(d => d.DeliverDate.ToDateTime("00:00")));

            #endregion

            #region مجوز تخلیه

            CreateMap<CreateUnloadingPermitCommand, UnloadingPermit>()
                .ForMember(m => m.EntrancePermitId, opt => opt.MapFrom(d=>d.TransferRemittanceEntrancePermitId))
                .ForMember(m => m.UnloadingPermitDetails, opt => opt.
                MapFrom(d => d.UnloadingPermitDetails));

            CreateMap<UnloadingPermit, UnloadingPermitViewModel>()
                    .ForMember(m => m.CreateDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()))
                    .ForMember(m => m.CreatorName, opt =>
                        opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))));

            CreateMap<UnloadingPermitDetail, UnloadingPermitDetailViewModel>();
            CreateMap<UnloadingPermitDetail, UnloadingPermitDetailViewModel>();

            CreateMap<UnloadingPermitDetailDto, UnloadingPermitDetail>();
            CreateMap<GetAllUnloadingPermitsParameter,GetAllUnloadingPermitsQuery>();
            CreateMap<GetAllUnloadingPermitsQuery,GetAllUnloadingPermitsParameter>();

            #endregion

            #region Costs
            CreateMap<Cost, CostViewModel>();

            CreateMap<CreateCostCommand, Cost>();

            CreateMap<UpdateCostCommand, Cost>();
            #endregion

            #region Incomes
            CreateMap<Income, IncomeViewModel>();

            CreateMap<CreateIncomeCommand, Income>();

            CreateMap<UpdateIncomeCommand, Income>();
            #endregion

            #region ShareHolders
            CreateMap<ShareHolder, ShareHolderViewModel>();

            CreateMap<CreateShareHolderCommand, ShareHolder>();

            CreateMap<UpdateShareHolderCommand, ShareHolder>()
                .ForMember(m => m.ShareHolderCode, opt => opt.Ignore());

            CreateMap<GetAllShareHoldersQuery, GetAllShareHoldersParameter>();

            #endregion

            #region LadingExitPermits            
            CreateMap<LadingExitPermit, LadingExitPermitViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(t => t.CreatedDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()))
                .ForMember(t => t.CreatedBy, opt => opt.MapFrom(d => string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName)));

            CreateMap<UpdateLadingExitPermitCommand, LadingExitPermit>();
            CreateMap<RevokeLadingExitPermitCommand, LadingExitPermit>();

            CreateMap<GetAllLadingExitPermitsQuery, GetAllLadingExitPermitsParameter>();
            CreateMap<CreateLadingExitPermitAttachment, LadingExitPermit>()
                .ForMember(m => m.LadingExitPermitDetails, opt => opt.Ignore());

            CreateMap<LadingExitPermitDetailDto, LadingExitPermitDetail>();
            CreateMap<LadingExitPermitDetail, LadingExitPermitDetailViewModel>()
                .ForMember(m => m.LadingAmount, opt => opt.MapFrom(d => d.CargoAnnounceDetail.LadingAmount))
                .ForMember(m => m.ProductMainUnitDesc, opt => opt.MapFrom(d => d.CargoAnnounceDetail.OrderDetail.Product.ProductMainUnit.UnitName))
                .ForMember(m => m.ProductSubUnitDesc, opt => opt.MapFrom(d => d.CargoAnnounceDetail.OrderDetail.Product.ProductSubUnit.UnitName))
                .ForMember(m => m.ProductSubUnitDesc, opt => opt.MapFrom(d => d.CargoAnnounceDetail.OrderDetail.Product.ProductSubUnit.UnitName))
                .ForMember(m => m.ProductName, opt => opt.MapFrom(d => d.CargoAnnounceDetail.OrderDetail.Product.ProductName))
                .ForMember(m => m.ProductBrandName, opt => opt.MapFrom(d => d.CargoAnnounceDetail.OrderDetail.ProductBrand.Brand.Name));


            CreateMap<CreateLadingExitPermitCommand, LadingExitPermit>()
                .ForMember(m => m.LadingExitPermitDetails, opt => opt.MapFrom(d => d.LadingExitPermitDetails))
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore());

            CreateMap<CreateLadingExitPermitDetailRequest, LadingExitPermitDetail>();

            #endregion

            #region RentPayments
            CreateMap<UnloadingPermit, RentsViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(m => m.ReferenceDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()))
                .ForMember(m => m.TotalAmount, opt => opt.MapFrom(d => d.FareAmount))
                .ForMember(m => m.ReferenceCode, opt => opt.MapFrom(d => d.UnloadingPermitCode))
                .ForMember(m => m.OtherCosts, opt => opt.MapFrom(d => d.OtherCosts))
                .ForMember(m => m.OrderTypeDesc, opt => opt.MapFrom(d => "سفارش خرید"))
                .ForMember(m => m.UnloadingPermitId, opt => opt.MapFrom(d => d.Id))
                .ForMember(m => m.CargoTotalWeight, opt =>
                opt.MapFrom(d => d.UnloadingPermitDetails.Sum(s => s.UnloadedAmount)))
                .ForMember(m => m.AccountOwnerName, opt => opt.MapFrom(d => ""));

            CreateMap<LadingExitPermit, RentsViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(m => m.ReferenceDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()))
                .ForMember(m => m.TotalAmount, opt => opt.MapFrom(d => d.FareAmount))
                .ForMember(m => m.ReferenceCode, opt => opt.MapFrom(d => d.LadingExitPermitCode))
                .ForMember(m => m.OtherCosts, opt => opt.MapFrom(d => d.OtherAmount))
                .ForMember(m => m.OrderTypeDesc, opt => opt.MapFrom(d => "سفارش فروش"))
                .ForMember(m => m.LadingExitPermitId, opt => opt.MapFrom(d => d.Id))
                .ForMember(m => m.TotalLadingAmount, opt =>
                opt.MapFrom(d => d.LadingExitPermitDetails.Sum(s => s.RealAmount)))
                .ForMember(m => m.AccountOwnerName, opt => opt.MapFrom(d => d.BankAccountOwnerName));

            CreateMap<LadingExitPermit, RentPaymentViewModel>();
            CreateMap<RentPayment, RentPaymentViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()))
                .ForMember(m => m.DriverName, opt => opt.MapFrom(d => d.LadingExitPermit != null ? d.LadingExitPermit.LadingPermit.CargoAnnounce.DriverName :
                                                               d.UnloadingPermit.DriverName))
                .ForMember(m => m.DriverMobile, opt => opt.MapFrom(d => d.LadingExitPermit != null ? d.LadingExitPermit.LadingPermit.CargoAnnounce.DriverMobile :
                                                               d.UnloadingPermit.DriverMobile))
                .ForMember(m => m.ReferenceCode, opt => opt.MapFrom(d => d.LadingExitPermit != null ? d.LadingExitPermit.LadingExitPermitCode :
                                                               d.UnloadingPermit.UnloadingPermitCode))
                .ForMember(m => m.DriverAccountNo, opt => opt.MapFrom(d => d.LadingExitPermit != null ? d.LadingExitPermit.BankAccountNo :
                                                               d.UnloadingPermit.DriverAccountNo))
                .ForMember(m => m.OtherCosts, opt => opt.MapFrom(d => d.LadingExitPermit != null ? d.LadingExitPermit.OtherAmount :
                                                               d.UnloadingPermit.OtherCosts))
                .ForMember(m => m.TotalFareAmount, opt => opt.MapFrom(d => d.LadingExitPermit != null ? d.LadingExitPermit.FareAmount :
                                                               d.UnloadingPermit.FareAmount))
                .ForMember(m => m.OrderType, opt => opt.MapFrom(d => d.LadingExitPermit != null ? "سفارش فروش" : "سفارش خرید"));

            CreateMap<CreateRentPaymentCommand, RentPayment>();
            CreateMap<RentPaymentDto, RentPayment>();

            CreateMap<UpdateRentPaymentCommand, RentPayment>();

            CreateMap<GetAllRentPaymentsQuery, GetAllRentPaymentsParameter>();
            CreateMap<GetAllRentsToPaymentQuery, GetAllRentsToPaymentParameter>();

            #endregion

            #region CashDesk
            CreateMap<CashDesk, CashDeskViewModel>();

            CreateMap<CreateCashDeskCommand, CashDesk>();

            CreateMap<UpdateCashDeskCommand, CashDesk>();
            CreateMap<GetAllCashDesksQuery, GetAllCashDesksParameter>();

            #endregion

            #region OrganizationBank
            CreateMap<Bank, BankViewModel>();
            CreateMap<OrganizationBank, OrganizationBankViewModel>()
                .ForMember(b => b.Bank, opt => opt.MapFrom(d => d.Bank));

            CreateMap<CreateOrganizationBankCommand, OrganizationBank>();

            CreateMap<UpdateOrganizationBankCommand, OrganizationBank>();

            #endregion

            #region PettyCash
            CreateMap<PettyCash, PettyCashViewModel>();
            CreateMap<CreatePettyCashCommand, PettyCash>();
            CreateMap<UpdatePettyCashCommand, PettyCash>();
            CreateMap<GetAllPettyCashsQuery, GetAllPettyCashsParameter>();

            #endregion

            CreateMap<Warehouse, ProductWarehouseViewModel>();

            #region
            CreateMap<AddRoleMenuDto, RoleMenu>()
                .ForMember(m => m.ApplicationRoleId, opt => opt.MapFrom(d => d.RoleId));

            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<GetAllApplicationUsersQuery, GetAllApplicationUsersParameter>();
            CreateMap<CreateApplicationUserCommand, ApplicationUser>();
            CreateMap<UpdateApplicationUserCommand, ApplicationUser>();

            CreateMap<ApplicationRole, ApplicationRoleViewModel>();
            CreateMap<GetAllApplicationRolesQuery, GetAllApplicationRolesParameter>();
            CreateMap<CreateApplicationRoleCommand, ApplicationRole>();
            CreateMap<UpdateApplicationRoleCommand, ApplicationRole>();

            CreateMap<Permission, PermissionViewModel>()
                .ForMember(m => m.ApplicationMenuName, opt => opt.MapFrom(d => d.ApplicationMenu.Description))
                .ForMember(m => m.PermissionName, opt => opt.MapFrom(d => d.Description));

            CreateMap<GetAllPermissionsQuery, GetAllPermissionsParameter>();
            CreateMap<CreatePermissionCommand, Permission>();
            CreateMap<UpdatePermissionCommand, Permission>();

            CreateMap<RolePermission, RolePermissionViewModel>()
                .ForMember(x => x.RoleName, opt => opt.MapFrom(d => d.ApplicationRole.Name))
                .ForMember(x => x.PermissionName, opt => opt.MapFrom(d => d.Permission.Name));

            CreateMap<GetAllRolePermissionsQuery, GetAllRolePermissionsParameter>();
            CreateMap<CreateRolePermissionCommand, RolePermission>();
            CreateMap<UpdateRolePermissionCommand, RolePermission>();

            CreateMap<UserRole, UserRoleViewModel>();
            CreateMap<GetAllUserRolesQuery, GetAllUserRolesParameter>();
            CreateMap<CreateUserRoleCommand, UserRole>();

            CreateMap<ApplicationMenu, PermissionsByMenuViewModel>()
                .ForMember(m => m.ApplicationMenuId, opt => opt.MapFrom(d => d.Id))
                .ForMember(m => m.ApplicationMenuName, opt => opt.MapFrom(d => d.Description));

            CreateMap<ApplicationMenu, ApplicationMenuViewModel>();
            CreateMap<GetAllRolePermissionsQuery, GetAllRolePermissionsParameter>();
            CreateMap<CreateRolePermissionCommand, RolePermission>();

            CreateMap<CreateUserRoleDto, UserRole>();
            CreateMap<ApplicationUserRoleDto, UserRole>();
            CreateMap<RolePermissionDto, RolePermission>();
            CreateMap<CreateRolePermissionDto, RolePermission>()
                .ForMember(m => m.Permission, opt => opt.Ignore());

            CreateMap<AddRoleMenuRequest, RoleMenu>();
            CreateMap<CreateUserRoleCommand, UserRole>();
            CreateMap<RoleMenu, RoleMenuViewModel>()
                .ForMember(r => r.RoleId, opt => opt.MapFrom(d => d.ApplicationRoleId))
                .ForMember(r => r.RoleName, opt => opt.MapFrom(d => d.ApplicationRole.Name));

            CreateMap<ApplicationMenu, ApplicationMenuViewModel>()
                .ForMember(m => m.Children, opt => opt.MapFrom(d => d.Children));

            //CreateMap<IdentityUserRole<Guid>, UserRoleViewModel>();
            #endregion

            #region Approve FarePayment
            CreateMap<ApproveLadingExitPermitFareAmountCommand, DriverFareAmountApprove>();
            CreateMap<ApprovePurOrderTransRemittFareAmountCommand, DriverFareAmountApprove>();
            #endregion

            #region برچسب های مشتری
            CreateMap<CustomerLabel, CustomerLabelViewModel>()
                .ForMember(m => m.LabelName, opt => opt.MapFrom(d =>
                d.Product!=null ? d.Product.ProductName : 
                d.Brand != null ? d.Brand.Name : 
                d.ProductType != null ? d.ProductType.Desc : 
                d.ProductBrand != null ? string.Concat(d.ProductBrand.Product.ProductName, ' ', d.ProductBrand.Brand.Name) : 
                d.LabelName
                ))
                .ForMember(m => m.ProductName, opt => opt.MapFrom(d => d.Product==null ? "": d.Product.ProductName))
                .ForMember(m => m.BrandName, opt => opt.MapFrom(d => d.Brand==null ? "": d.Brand.Name))
                .ForMember(m => m.ProductTypeName, opt => opt.MapFrom(d => d.ProductType==null ? "": d.ProductType.Desc))
                .ForMember(m => m.CustomerLabelTypeDesc, opt => opt.MapFrom(d => d.CustomerLabelType.LabelTypeDesc))
                .ForMember(m => m.ProductBrandName, opt => opt.MapFrom(d =>  (d.ProductBrand==null ? "": string.Concat(d.ProductBrand.Product.ProductName,' ', d.ProductBrand.Brand.Name))));

            CreateMap<CreateCustomerLabelCommand, CustomerLabel>();
            CreateMap<UpdateCustomerLabelCommand, CustomerLabel>();
            CreateMap<GetAllCustomerLabelsQuery, GetAllCustomerLabelsParameter>();
            #endregion

            #region Personnels پرسنل
            CreateMap<CreatePersonnelCommand, Personnel>()
                .ForMember(m => m.Phonebook, opt => opt.MapFrom(d => d.Phonebook));

            CreateMap<Personnel, PersonnelViewModel>();

            CreateMap<GetAllPersonnelsQuery, GetAllPersonnelsParameter>();

            CreateMap<UpdatePersonnelCommand, Personnel>()
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore());

            #endregion

            #region TransferWarehouseInventories انتقال موجودی انبار
            CreateMap<CreateTransferWarehouseInventoryCommand, TransferWarehouseInventory>();
            CreateMap<TransferWarehouseInventoryDetailDto, TransferWarehouseInventoryDetail>();

            CreateMap<TransferWarehouseInventory, TransferWarehouseInventoryViewModel>()
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()));

            CreateMap<TransferWarehouseInventoryDetail, TransferInventoryDetailViewModel>()
                .ForMember(m => m.ProductName, opt => opt.MapFrom(d => d.ProductBrand.Product.ProductName))
                .ForMember(m => m.BrandName, opt => opt.MapFrom(d => d.ProductBrand.Brand.Name));

            CreateMap<GetAllTransferWarehouseInventoriesQuery, GetAllTransferWarehouseInventoriesParameter>();

            CreateMap<UpdateTransferWarehouseInventoryCommand, TransferWarehouseInventory>()
                .ForMember(m => m.Created, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore());

            #endregion

            #region PaymentRequests
            CreateMap<PaymentRequest, PaymentRequestViewModel>()
                .ForMember(m => m.PaymentRequestReasonDesc, opt => opt.MapFrom(d => d.PaymentRequestReason.ReasonDesc))
                .ForMember(m => m.BankName, opt => opt.MapFrom(d => string.Concat(d.Bank.BankName)))
                .ForMember(m => m.PaymentRequestStatusDesc, opt => opt.MapFrom(d => string.Concat(d.PaymentRequestStatus.StatusDesc)))
                .ForMember(m => m.CustomerName, opt => opt.MapFrom(d => string.Concat(d.Customer.FirstName, " ", d.Customer.LastName)))
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()));

            CreateMap<ApprovePaymentRequestCommand, PaymentRequest>();
            CreateMap<RejectPaymentRequestCommand, PaymentRequest>();
            CreateMap<ProceedToPaymentRequestCommand, PaymentRequest>();
            CreateMap<CreatePaymentRequestCommand, PaymentRequest>();

            CreateMap<UpdatePaymentRequestCommand, PaymentRequest>();

            CreateMap<GetAllPaymentRequestsQuery, GetAllPaymentRequestsParameter>();

            #endregion

            #region PersonnelPaymentRequests درخواست پرداخت پرسنل
            CreateMap<PersonnelPaymentRequest, PersonnelPaymentRequestViewModel>()
                .ForMember(m => m.PaymentRequestReasonDesc, opt => opt.MapFrom(d => d.PaymentRequestReason.ReasonDesc))
                .ForMember(m => m.BankName, opt => opt.MapFrom(d => string.Concat(d.Bank.BankName)))
                .ForMember(m => m.PaymentRequestStatusDesc, opt => opt.MapFrom(d => string.Concat(d.PaymentRequestStatus.StatusDesc)))
                .ForMember(m => m.CustomerName, opt => opt.MapFrom(d => string.Concat(d.Customer.FirstName, " ", d.Customer.LastName)))
                .ForMember(m => m.CreatorName, opt => opt.MapFrom(d => d.ApplicationUser == null ? "" : (string.Concat(d.ApplicationUser.FirstName, " ", d.ApplicationUser.LastName))))
                .ForMember(m => m.CreatedDate, opt => opt.MapFrom(d => d.Created.ToShamsiDate()));

            CreateMap<ApprovePersonnelPaymentRequestCommand, PersonnelPaymentRequest>();
            CreateMap<RejectPersonnelPaymentRequestCommand, PersonnelPaymentRequest>();
            CreateMap<ProceedToPersonnelPaymentRequestCommand, PersonnelPaymentRequest>();
            CreateMap<CreatePersonnelPaymentRequestCommand, PersonnelPaymentRequest>();

            CreateMap<UpdatePersonnelPaymentRequestCommand, PersonnelPaymentRequest>();

            CreateMap<GetAllPersonnelPaymentRequestsQuery, GetAllPersonnelPaymentRequestsParameter>();

            #endregion



        }

        private List<CustomerAssignedLabel> ConvertToCustomerLabels(IEnumerable<int> assignedLabels)
        {
            throw new NotImplementedException();
        }

        private List<Attachment> ConvertAttachment(List<string>? attachments)
        {
            List<Attachment> result = new List<Attachment>();
            foreach (var item in attachments)
            {
                result.Add(new Attachment
                {
                    FileData = Convert.FromBase64String(item)
                });
            }

            return result;
        }

        public List<Attachment> ConvertToByte(IList<IFormFile> fileData)
        {
            List<Attachment> attachments = new List<Attachment>();
            foreach (var file in fileData)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string s = Convert.ToBase64String(fileBytes);
                        attachments.Add(new Attachment { FileData = fileBytes });
                    }
                }
            }

            return attachments;
        }

        public byte[] ConvertToByte(string fileData)
        {
            return Convert.FromBase64String(fileData);
        }

        //public async Task<string> GetUserName(Guid? userId)
        //{
        //    if (userId == null)
        //        return "";

        //    var user=await _applicationUser.GetApplicationUserInfo((Guid)userId);
        //    return string.Concat(user.FirstName, " ", user.LastName);
        //}

    }
}