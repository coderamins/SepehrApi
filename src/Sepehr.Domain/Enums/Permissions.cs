using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Enums
{
    public enum Permissions
    {
        #region User Controller
        GetAllUsers = 1,
        GetUserById,
        GetLoginedUserInfo,
        CreateUser,
        UpdateUser,
        DeleteUser,
        #endregion

        #region Service Controller
        GetAllServices,
        GetServiceById,
        CreateService,
        UpdateService,
        DeleteService,
        #endregion

        #region RolePermission Controller
        GetRolePermissions,
        GetRolePermissionById,
        CreateRolePermission,
        UpdateRolePermission,
        DeleteRolePermission,
        #endregion

        #region RoleMenu Controller
        CreateRoleMenu,
        GetRoleMenuById,
        GetUserApplicationMenus,
        GetAllApplicationMenus,
        DeleteApplicationMenu,

        #endregion

        #region ReceivePay Controller
        GetAllReceivePays,
        GetReceivePayById,
        CreateReceivePay,
        UpdateReceivePay,
        DeleteReceivePay,
        ReceivePayApprove,
        #endregion

        #region PurchaseOrder Controller
        GetAllPurchaseOrders,
        GetPurchaseOrderById,
        GetPurchaseOrderInfo,
        CreatePurchaseOrder,
        UpdatePurchaseOrder,
        DeletePurchaseOrder,
        #endregion

        #region ProductType Controller
        GetAllProductTypes,
        GetProductTypeById,
        CreateProductType,
        UpdateProductType,
        DeleteProductType,
        #endregion

        #region ProductSupplier Controller
        GetAllProductSuppliers,
        GetProductSupplierById,
        CreateProductSupplier,
        UpdateProductSupplier,
        DeleteProductSupplier,
        #endregion

        #region ProductState Controller
        GetAllProductStates,
        GetProductStateById,
        CreateProductState,
        UpdateProductState,
        DeleteProductState,
        #endregion

        #region ProductStandard Controller
        GetAllProductStandards,
        GetProductStandardById,
        CreateProductStandard,
        UpdateProductStandard,
        DeleteProductStandard,
        #endregion

        #region ProductPrice Controller
        GetAllProductPrices,
        ExportProductPrices,
        GetProductPriceById,
        CreateProductPrice,
        CreateProductPriceFromFile,
        UpdateProductPrice,
        DeleteProductPrice,
        #endregion

        #region Product Controller
        GetAllProducts,
        GetAllProductsByType,
        GetProductsById,
        CreateProduct,
        UpdateProduct,
        EnableProduct,
        DeleteProduct,
        #endregion

        #region ProductBrand Controller
        GetAllProductBrands,
        GetProductBrandById,
        CreateProductBran,
        UpdateProductBrand,
        DeleteProductBrand,
        #endregion

        #region Permission Controller
        GetAllPermissions,
        GetPermissionById,
        CreatePermission,
        UpdatePermission,
        DeletePermission,

        #endregion

        #region User Controller
        GetAllOrders,
        GetOrderByOrderCode,
        CreateOrder,
        UpdateOrder,
        ConfirmOrder,
        DeleteOrder,
        ApproveOrderInvoiceType,
        ReturnOrder,
        CompleteOrderAnnouncement,
        #endregion

        #region User Controller
        GetAllLadingLicenses,
        GetLadingLicenseById,
        CreateLadingLicense,
        UpdateLadingLicense,
        DeleteLadingLicense,
        LadingExitPermit,
        #endregion

        #region Generic Controller
        GetOrderSendTypes,
        GetRentPaymentTypes,
        GetPurchaseInvoices,
        GetInvoiceTypes,
        GetCustomerValidities,
        GetWarehouseTypes,
        GetWarehouses,
        GetReceivePaymentSources,
        GetProductUnits,
        GetServices,
        GetProductTypes,
        GetOrderStatuses,
        GetVehicleTypes,
        #endregion

        #region CustomerOfficial Controller
        GetAllCustomerOfficialCompanies,
        GetCustomerOfficialCompanyById,
        CreateCustomerOfficialCompany,
        UpdateCustomerOfficialCompany,
        DeleteCustomerOfficialCompany,
        #endregion

        #region Customer Controller
        GetAllCustomers,
        GetCustomerById,
        CreateCustomer,
        UpdateCustomer,
        DeleteCustomer,
        AllocateCustomerWarehouses,
        GetCustomerWarehouses,
        #endregion

        #region CargoAnnouncement Controller
        GetAllCargoAnnouncements,
        GetCargoAnnouncementById,
        CreateCargoAnnouncement,
        UpdateCargoAnnouncement,
        DeleteCargoAnnouncement,
        GetNotAnnouncedOrders,
        #endregion

        #region Brand Controller
        GetAllBrands,
        GetBrandById,
        CreateBrand,
        UpdateBrand,
        DeleteBrand,
        #endregion

        #region UserRole Controller
        GetAllUserRoles,
        CreateUserRole,
        DeleteUserRole,
        #endregion

        #region ApplicationRole Controller
        GetAllApplicationRoles,
        GetApplicationRoleById,
        CreateApplicationRole,
        UpdateApplicationRole,
        DeleteApplicationRole,
        #endregion

        #region Warehouse Controller
        GetAllWarehouses,
        GetWarehouseById,
        CreateWarehouse,
        UpdateWarehouse,
        DeleteWarehouse,
        #endregion

        #region Product Inventory
        UploadProductInventory,
        GetProductInventoriesExcelReport,
        #endregion

        #region ShareHolder
        GetAllShareHolders,
        GetShareHolderById,
        CreateShareHolder,
        UpdateShareHolder,
        DeleteShareHolder,

        #endregion

        #region ShareHolder
        GetAllCosts,
        GetCostById,
        CreateCost,
        UpdateCost,
        DeleteCost
        #endregion
    }
}
