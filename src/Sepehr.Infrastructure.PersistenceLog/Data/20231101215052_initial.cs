using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.PersistenceLog.Data
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sepdb");

            migrationBuilder.CreateTable(
                name: "CargoAnnouncementLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnloadingPlaceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarPlaque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverMobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ApprovedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    MainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoAnnouncementLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerCode = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSupplier = table.Column<bool>(type: "bit", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerType = table.Column<int>(type: "int", nullable: false),
                    CustomerValidityId = table.Column<int>(type: "int", nullable: false),
                    Tel1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tel2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Representative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementType = table.Column<int>(type: "int", nullable: false),
                    SettlementDay = table.Column<int>(type: "int", nullable: false),
                    MainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LadingLicenseDetailLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LadingLicenseId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApproximateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RealAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackageCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadingLicenseDetailLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    ProximateAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PackageNumber = table.Column<int>(type: "int", nullable: false),
                    NumberInPackage = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CargoSendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerCompanyRow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaserCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseInvoiceTypeId = table.Column<int>(type: "int", nullable: true),
                    PurchaseSettlementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternativeProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AlternativeProductAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlternativeProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MainId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderCode = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConfirmedStatus = table.Column<bool>(type: "bit", nullable: false),
                    ExitType = table.Column<int>(type: "int", nullable: false),
                    OrderSendTypeId = table.Column<int>(type: "int", nullable: false),
                    OrderTypeId = table.Column<int>(type: "int", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    CustomerOfficialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FreightName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SettlementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    SelectedProductUnitId = table.Column<int>(type: "int", nullable: true),
                    DischargePlaceAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreightDriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarPlaque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompletlySended = table.Column<bool>(type: "bit", nullable: false),
                    IsApprovedInvoiceType = table.Column<bool>(type: "bit", nullable: false),
                    ApprovingUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderPaymentLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaysAfterExit = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    MainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPaymentLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderServiceLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderServiceLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductBrandLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    MainId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrandLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetailLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Standard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetailLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventorieLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    Thickness = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    ApproximateInventory = table.Column<double>(type: "float", nullable: false),
                    FloorInventory = table.Column<double>(type: "float", nullable: false),
                    MaxInventory = table.Column<double>(type: "float", nullable: false),
                    MinInventory = table.Column<double>(type: "float", nullable: false),
                    OrderPoint = table.Column<double>(type: "float", nullable: false),
                    MainId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventorieLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCode = table.Column<long>(type: "bigint", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: true),
                    ProductSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductThickness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproximateWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberInPackage = table.Column<int>(type: "int", nullable: false),
                    ProductStandardId = table.Column<int>(type: "int", nullable: true),
                    ProductStateId = table.Column<int>(type: "int", nullable: true),
                    ProductMainUnitId = table.Column<int>(type: "int", nullable: false),
                    ProductSubUnitId = table.Column<int>(type: "int", nullable: true),
                    ExchangeRate = table.Column<double>(type: "float", nullable: true),
                    MaxInventory = table.Column<int>(type: "int", nullable: false),
                    MinInventory = table.Column<int>(type: "int", nullable: false),
                    InventotyCriticalPoint = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductPriceLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPriceLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSupplierLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OverPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    MainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSupplierLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceivePayLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiveFromCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceivePayCode = table.Column<long>(type: "bigint", nullable: false),
                    PayToCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceivePaymentSourceFromId = table.Column<int>(type: "int", nullable: true),
                    ReceivePaymentSourceToId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrachingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAccountingApproval = table.Column<bool>(type: "bit", nullable: false),
                    AccountingApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountingApproverId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivePayLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceivePaymentSourceLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivePaymentSourceLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableRecordRemovalInfoLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RemovedRecordId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableRecordRemovalInfoLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CargoAnnouncementLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "CustomerLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "LadingLicenseDetailLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderDetailLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderPaymentLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderServiceLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductBrandLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductDetailLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductInventorieLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductPriceLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductSupplierLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ReceivePayLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ReceivePaymentSourceLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "TableRecordRemovalInfoLogs",
                schema: "sepdb");
        }
    }
}
