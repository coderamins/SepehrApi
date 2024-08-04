using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202407251030pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sepdb");

            migrationBuilder.CreateTable(
                name: "Banks",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashDesks",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashDeskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashDesks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Costs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerValidities",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidityDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerValidities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncomeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderExitTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ExitTypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderExitTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderSendTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSendTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PettyCashs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PettyCashDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PettyCashs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductStandards",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStandards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductStates",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductCodeSeedStart = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductUnits",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInvoiceTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderFarePaymentTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderFarePaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderSendTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SendTypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderSendTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderStatus",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceivePaymentTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivePaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceivePayStatus",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivePayStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferRemittanceStatus",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StatusDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferRemittanceStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferRemittanceTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RemittanceTypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferRemittanceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsDealerUser = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseTypes",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationBanks",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountOwner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationBanks_Banks_BankId",
                        column: x => x.BankId,
                        principalSchema: "sepdb",
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationMenus",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuIcon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationMenus_ApplicationMenus_ApplicationMenuId",
                        column: x => x.ApplicationMenuId,
                        principalSchema: "sepdb",
                        principalTable: "ApplicationMenus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApplicationMenus_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerCode = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    CustomerCharacteristics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerValidities_CustomerValidityId",
                        column: x => x.CustomerValidityId,
                        principalSchema: "sepdb",
                        principalTable: "CustomerValidities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductInventoryHistories",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productBrandId = table.Column<int>(type: "int", nullable: false),
                    productCode = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    ApproximateInventory = table.Column<int>(type: "int", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    priceDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventoryHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInventoryHistories_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShareHolders",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShareHolderCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "3030, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareHolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareHolders_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "sepdb",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_WarehouseTypes_WarehouseTypeId",
                        column: x => x.WarehouseTypeId,
                        principalSchema: "sepdb",
                        principalTable: "WarehouseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_ApplicationMenus_ApplicationMenuId",
                        column: x => x.ApplicationMenuId,
                        principalSchema: "sepdb",
                        principalTable: "ApplicationMenus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RoleMenus",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMenus_ApplicationMenus_ApplicationMenuId",
                        column: x => x.ApplicationMenuId,
                        principalSchema: "sepdb",
                        principalTable: "ApplicationMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleMenus_Roles_ApplicationRoleId",
                        column: x => x.ApplicationRoleId,
                        principalSchema: "sepdb",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOfficialCompanies",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EconomicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    CustomerType = table.Column<int>(type: "int", nullable: true),
                    Tel1 = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Tel2 = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOfficialCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerOfficialCompanies_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerOfficialCompanies_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Phonebook",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PhoneNumberTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phonebook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phonebook_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phonebook_PhoneNumberTypes_PhoneNumberTypeId",
                        column: x => x.PhoneNumberTypeId,
                        principalSchema: "sepdb",
                        principalTable: "PhoneNumberTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Phonebook_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerWarehouses",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerWarehouses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerWarehouses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "sepdb",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCode = table.Column<int>(type: "int", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: true),
                    ProductSize = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductThickness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproximateWeight = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    NumberInPackage = table.Column<int>(type: "int", nullable: false),
                    ProductStandardId = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    ProductStateId = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    ProductMainUnitId = table.Column<int>(type: "int", nullable: false),
                    ProductSubUnitId = table.Column<int>(type: "int", nullable: true),
                    ExchangeRate = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductStandards_ProductStandardId",
                        column: x => x.ProductStandardId,
                        principalSchema: "sepdb",
                        principalTable: "ProductStandards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductStates_ProductStateId",
                        column: x => x.ProductStateId,
                        principalSchema: "sepdb",
                        principalTable: "ProductStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalSchema: "sepdb",
                        principalTable: "ProductTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductUnits_ProductMainUnitId",
                        column: x => x.ProductMainUnitId,
                        principalSchema: "sepdb",
                        principalTable: "ProductUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductUnits_ProductSubUnitId",
                        column: x => x.ProductSubUnitId,
                        principalSchema: "sepdb",
                        principalTable: "ProductUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "sepdb",
                        principalTable: "Warehouses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransferRemittances",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginWarehouseId = table.Column<int>(type: "int", nullable: false),
                    DestinationWarehouseId = table.Column<int>(type: "int", nullable: false),
                    TransferRemittanceTypeId = table.Column<int>(type: "int", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plaque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: true),
                    DriverMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverCreditCardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliverDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FareAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    FareAmountApproved = table.Column<bool>(type: "bit", nullable: false),
                    OtherCosts = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    TransferRemittanceStatusId = table.Column<int>(type: "int", nullable: false),
                    UnloadingPlaceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferRemittances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferRemittances_TransferRemittanceStatus_TransferRemittanceStatusId",
                        column: x => x.TransferRemittanceStatusId,
                        principalSchema: "sepdb",
                        principalTable: "TransferRemittanceStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferRemittances_TransferRemittanceTypes_TransferRemittanceTypeId",
                        column: x => x.TransferRemittanceTypeId,
                        principalSchema: "sepdb",
                        principalTable: "TransferRemittanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferRemittances_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferRemittances_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalSchema: "sepdb",
                        principalTable: "VehicleTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferRemittances_Warehouses_DestinationWarehouseId",
                        column: x => x.DestinationWarehouseId,
                        principalSchema: "sepdb",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferRemittances_Warehouses_OriginWarehouseId",
                        column: x => x.OriginWarehouseId,
                        principalSchema: "sepdb",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "sepdb",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "sepdb",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderCode = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    OrderSendTypeId = table.Column<int>(type: "int", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    FarePaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: false),
                    CustomerOfficialCompanyId = table.Column<int>(type: "int", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceApproveDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTemporary = table.Column<bool>(type: "bit", nullable: false),
                    OrderTypeId = table.Column<int>(type: "int", nullable: false),
                    DeliverDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderExitTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_CustomerOfficialCompanies_CustomerOfficialCompanyId",
                        column: x => x.CustomerOfficialCompanyId,
                        principalSchema: "sepdb",
                        principalTable: "CustomerOfficialCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_InvoiceTypes_InvoiceTypeId",
                        column: x => x.InvoiceTypeId,
                        principalSchema: "sepdb",
                        principalTable: "InvoiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderExitTypes_OrderExitTypeId",
                        column: x => x.OrderExitTypeId,
                        principalSchema: "sepdb",
                        principalTable: "OrderExitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderSendTypes_OrderSendTypeId",
                        column: x => x.OrderSendTypeId,
                        principalSchema: "sepdb",
                        principalTable: "OrderSendTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalSchema: "sepdb",
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_PaymentTypes_FarePaymentTypeId",
                        column: x => x.FarePaymentTypeId,
                        principalSchema: "sepdb",
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExitType = table.Column<int>(type: "int", nullable: false),
                    OriginWarehouseId = table.Column<int>(type: "int", nullable: false),
                    DestinationWarehouseId = table.Column<int>(type: "int", nullable: false),
                    IsIntermediary = table.Column<bool>(type: "bit", nullable: false),
                    ApprovingUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OrderCode = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    OrderSendTypeId = table.Column<int>(type: "int", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    FarePaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    InvoiceTypeId = table.Column<int>(type: "int", nullable: false),
                    CustomerOfficialCompanyId = table.Column<int>(type: "int", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceApproveDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTemporary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_CustomerOfficialCompanies_CustomerOfficialCompanyId",
                        column: x => x.CustomerOfficialCompanyId,
                        principalSchema: "sepdb",
                        principalTable: "CustomerOfficialCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_InvoiceTypes_InvoiceTypeId",
                        column: x => x.InvoiceTypeId,
                        principalSchema: "sepdb",
                        principalTable: "InvoiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_PurchaseOrderFarePaymentTypes_FarePaymentTypeId",
                        column: x => x.FarePaymentTypeId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrderFarePaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_PurchaseOrderSendTypes_OrderSendTypeId",
                        column: x => x.OrderSendTypeId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrderSendTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_PurchaseOrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Warehouses_DestinationWarehouseId",
                        column: x => x.DestinationWarehouseId,
                        principalSchema: "sepdb",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Warehouses_OriginWarehouseId",
                        column: x => x.OriginWarehouseId,
                        principalSchema: "sepdb",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceivePays",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceivePaymentTypeFromId = table.Column<int>(type: "int", nullable: true),
                    ReceivePaymentTypeToId = table.Column<int>(type: "int", nullable: true),
                    ReceivePayCode = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    AccountOwner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrachingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAccountingApproval = table.Column<bool>(type: "bit", nullable: false),
                    AccountingApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountingApproverId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountingDocNo = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountingDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivePayStatusId = table.Column<int>(type: "int", nullable: false),
                    ChequeNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChequeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceiveFromCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiveFromOrganizationBankId = table.Column<int>(type: "int", nullable: true),
                    ReceiveFromCashDeskId = table.Column<int>(type: "int", nullable: true),
                    ReceiveFromIncomeId = table.Column<int>(type: "int", nullable: true),
                    ReceiveFromPettyCashId = table.Column<int>(type: "int", nullable: true),
                    ReceiveFromCostId = table.Column<int>(type: "int", nullable: true),
                    ReceiveFromShareHolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayToCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayToOrganizationBankId = table.Column<int>(type: "int", nullable: true),
                    PayToCashDeskId = table.Column<int>(type: "int", nullable: true),
                    PayToIncomeId = table.Column<int>(type: "int", nullable: true),
                    PayToPettyCashId = table.Column<int>(type: "int", nullable: true),
                    PayToCostId = table.Column<int>(type: "int", nullable: true),
                    PayToShareHolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReceiveFromCompanyId = table.Column<int>(type: "int", nullable: true),
                    PayToCompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivePays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceivePays_CashDesks_PayToCashDeskId",
                        column: x => x.PayToCashDeskId,
                        principalSchema: "sepdb",
                        principalTable: "CashDesks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_CashDesks_ReceiveFromCashDeskId",
                        column: x => x.ReceiveFromCashDeskId,
                        principalSchema: "sepdb",
                        principalTable: "CashDesks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_Costs_PayToCostId",
                        column: x => x.PayToCostId,
                        principalSchema: "sepdb",
                        principalTable: "Costs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_Costs_ReceiveFromCostId",
                        column: x => x.ReceiveFromCostId,
                        principalSchema: "sepdb",
                        principalTable: "Costs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_CustomerOfficialCompanies_PayToCompanyId",
                        column: x => x.PayToCompanyId,
                        principalSchema: "sepdb",
                        principalTable: "CustomerOfficialCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_CustomerOfficialCompanies_ReceiveFromCompanyId",
                        column: x => x.ReceiveFromCompanyId,
                        principalSchema: "sepdb",
                        principalTable: "CustomerOfficialCompanies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_Customers_PayToCustomerId",
                        column: x => x.PayToCustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_Customers_ReceiveFromCustomerId",
                        column: x => x.ReceiveFromCustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_Incomes_PayToIncomeId",
                        column: x => x.PayToIncomeId,
                        principalSchema: "sepdb",
                        principalTable: "Incomes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_Incomes_ReceiveFromIncomeId",
                        column: x => x.ReceiveFromIncomeId,
                        principalSchema: "sepdb",
                        principalTable: "Incomes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_OrganizationBanks_PayToOrganizationBankId",
                        column: x => x.PayToOrganizationBankId,
                        principalSchema: "sepdb",
                        principalTable: "OrganizationBanks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_OrganizationBanks_ReceiveFromOrganizationBankId",
                        column: x => x.ReceiveFromOrganizationBankId,
                        principalSchema: "sepdb",
                        principalTable: "OrganizationBanks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_PettyCashs_PayToPettyCashId",
                        column: x => x.PayToPettyCashId,
                        principalSchema: "sepdb",
                        principalTable: "PettyCashs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_PettyCashs_ReceiveFromPettyCashId",
                        column: x => x.ReceiveFromPettyCashId,
                        principalSchema: "sepdb",
                        principalTable: "PettyCashs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_ReceivePayStatus_ReceivePayStatusId",
                        column: x => x.ReceivePayStatusId,
                        principalSchema: "sepdb",
                        principalTable: "ReceivePayStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceivePays_ReceivePaymentTypes_ReceivePaymentTypeFromId",
                        column: x => x.ReceivePaymentTypeFromId,
                        principalSchema: "sepdb",
                        principalTable: "ReceivePaymentTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_ReceivePaymentTypes_ReceivePaymentTypeToId",
                        column: x => x.ReceivePaymentTypeToId,
                        principalSchema: "sepdb",
                        principalTable: "ReceivePaymentTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_ShareHolders_PayToShareHolderId",
                        column: x => x.PayToShareHolderId,
                        principalSchema: "sepdb",
                        principalTable: "ShareHolders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_ShareHolders_ReceiveFromShareHolderId",
                        column: x => x.ReceiveFromShareHolderId,
                        principalSchema: "sepdb",
                        principalTable: "ShareHolders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceivePays_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfficialWarehoseInventories",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Thickness = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    ApproximateInventory = table.Column<double>(type: "float", nullable: false),
                    FloorInventory = table.Column<double>(type: "float", nullable: false),
                    MaxInventory = table.Column<double>(type: "float", nullable: false),
                    MinInventory = table.Column<double>(type: "float", nullable: false),
                    OrderPoint = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialWarehoseInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficialWarehoseInventories_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sepdb",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfficialWarehoseInventories_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OfficialWarehoseInventories_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "sepdb",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductBrands",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductBrands_Brands_BrandId",
                        column: x => x.BrandId,
                        principalSchema: "sepdb",
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBrands_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sepdb",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
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
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sepdb",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSuppliers",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    RentAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    OverPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PriceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSuppliers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSuppliers_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sepdb",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSuppliers_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntrancePermits",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermitCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    TransferRemittanceId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntrancePermits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntrancePermits_TransferRemittances_TransferRemittanceId",
                        column: x => x.TransferRemittanceId,
                        principalSchema: "sepdb",
                        principalTable: "TransferRemittances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntrancePermits_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderPayments",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaysAfterExit = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPayments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderServices",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderServices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "sepdb",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CargoAnnounces",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CargoAnnounceNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnloadingPlaceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarPlaque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverMobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ApprovedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FareAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasLadingPermit = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoAnnounces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoAnnounces_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CargoAnnounces_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CargoAnnounces_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CargoAnnounces_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalSchema: "sepdb",
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderPayments",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DaysAfterExit = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderPayments_PurchaseOrder_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderServices",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderServices_PurchaseOrder_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "sepdb",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderTransfers",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderTransfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderTransfers_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderTransfers_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RowId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    ProximateAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PackageNumber = table.Column<int>(type: "int", nullable: false),
                    NumberInPackage = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerCompanyRow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaserCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PurchaseInvoiceTypeId = table.Column<int>(type: "int", nullable: true),
                    PurchaseSettlementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlternativeProductBrandId = table.Column<int>(type: "int", nullable: true),
                    AlternativeProductAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    AlternativeProductPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ProductSubUnitId = table.Column<int>(type: "int", nullable: true),
                    ProductSubUnitAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Customers_PurchaserCustomerId",
                        column: x => x.PurchaserCustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_ProductBrands_AlternativeProductBrandId",
                        column: x => x.AlternativeProductBrandId,
                        principalSchema: "sepdb",
                        principalTable: "ProductBrands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalSchema: "sepdb",
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_ProductUnits_ProductSubUnitId",
                        column: x => x.ProductSubUnitId,
                        principalSchema: "sepdb",
                        principalTable: "ProductUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sepdb",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_PurchaseInvoiceTypes_PurchaseInvoiceTypeId",
                        column: x => x.PurchaseInvoiceTypeId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseInvoiceTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "sepdb",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventories",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    ApproximateInventory = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    OnTransitInventory = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ReserveInventory = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PurchaseInventory = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    FloorInventory = table.Column<double>(type: "float", nullable: false),
                    MaxInventory = table.Column<double>(type: "float", nullable: false),
                    MinInventory = table.Column<double>(type: "float", nullable: false),
                    Thickness = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    OrderPoint = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInventories_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalSchema: "sepdb",
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInventories_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sepdb",
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductInventories_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductInventories_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "sepdb",
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrices_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalSchema: "sepdb",
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "sepdb",
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductPrices_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetails",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseInvoiceTypeId = table.Column<int>(type: "int", nullable: true),
                    PurchaserCustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowId = table.Column<int>(type: "int", nullable: false),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    ProximateAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TransferedAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PackageNumber = table.Column<int>(type: "int", nullable: false),
                    NumberInPackage = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    DeliverDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternativeProductBrandId = table.Column<int>(type: "int", nullable: true),
                    AlternativeProductAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    AlternativeProductPrice = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ProductSubUnitId = table.Column<int>(type: "int", nullable: true),
                    ProductSubUnitAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_Customers_PurchaserCustomerId",
                        column: x => x.PurchaserCustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_ProductBrands_AlternativeProductBrandId",
                        column: x => x.AlternativeProductBrandId,
                        principalSchema: "sepdb",
                        principalTable: "ProductBrands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalSchema: "sepdb",
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_ProductUnits_ProductSubUnitId",
                        column: x => x.ProductSubUnitId,
                        principalSchema: "sepdb",
                        principalTable: "ProductUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_PurchaseInvoiceTypes_PurchaseInvoiceTypeId",
                        column: x => x.PurchaseInvoiceTypeId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseInvoiceTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_PurchaseOrder_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransferRemittanceDetails",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductBrandId = table.Column<int>(type: "int", nullable: false),
                    TransferAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TransferRemittanceId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferRemittanceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferRemittanceDetails_ProductBrands_ProductBrandId",
                        column: x => x.ProductBrandId,
                        principalSchema: "sepdb",
                        principalTable: "ProductBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferRemittanceDetails_TransferRemittances_TransferRemittanceId",
                        column: x => x.TransferRemittanceId,
                        principalSchema: "sepdb",
                        principalTable: "TransferRemittances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LadingPermit",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoAnnounceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasExitPermit = table.Column<bool>(type: "bit", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadingPermit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LadingPermit_CargoAnnounces_CargoAnnounceId",
                        column: x => x.CargoAnnounceId,
                        principalSchema: "sepdb",
                        principalTable: "CargoAnnounces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingPermit_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LadingPermit_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LadingPermit_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CargoAnnounceDetails",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoAnnounceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    LadingAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    RealAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PackageCount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargoAnnounceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CargoAnnounceDetails_CargoAnnounces_CargoAnnounceId",
                        column: x => x.CargoAnnounceId,
                        principalSchema: "sepdb",
                        principalTable: "CargoAnnounces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CargoAnnounceDetails_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalSchema: "sepdb",
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CargoAnnounceDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderTransferDetails",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderDetailId = table.Column<int>(type: "int", nullable: false),
                    TransferedAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PurchaseOrderTransferId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderTransferDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderTransferDetails_PurchaseOrderDetails_PurchaseOrderDetailId",
                        column: x => x.PurchaseOrderDetailId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderTransferDetails_PurchaseOrderTransfers_PurchaseOrderTransferId",
                        column: x => x.PurchaseOrderTransferId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrderTransfers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnloadingPermits",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntrancePermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnloadingPermitCode = table.Column<int>(type: "int", nullable: false),
                    TransferRemittanceDetailId = table.Column<int>(type: "int", nullable: false),
                    DriverAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverCreditCardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherCosts = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FareAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    FareAmountApproved = table.Column<bool>(type: "bit", nullable: false),
                    FareAmountPayStatus = table.Column<bool>(type: "bit", nullable: false),
                    ShippingName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plaque = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    DriverMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliverDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnloadingPlaceAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnloadingPermits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnloadingPermits_EntrancePermits_EntrancePermitId",
                        column: x => x.EntrancePermitId,
                        principalSchema: "sepdb",
                        principalTable: "EntrancePermits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnloadingPermits_TransferRemittanceDetails_TransferRemittanceDetailId",
                        column: x => x.TransferRemittanceDetailId,
                        principalSchema: "sepdb",
                        principalTable: "TransferRemittanceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnloadingPermits_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LadingExitPermit",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LadingExitPermitCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    LadingPermitId = table.Column<int>(type: "int", nullable: false),
                    BankAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditCardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ExitPermitDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FareAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    FareAmountPayStatus = table.Column<bool>(type: "bit", nullable: false),
                    HasExitPermit = table.Column<bool>(type: "bit", nullable: true),
                    FareAmountApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadingExitPermit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LadingExitPermit_LadingPermit_LadingPermitId",
                        column: x => x.LadingPermitId,
                        principalSchema: "sepdb",
                        principalTable: "LadingPermit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingExitPermit_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LadingPermitDetail",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LadingPermitId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    LadingAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    RealAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PackageCount = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderDetailId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadingPermitDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LadingPermitDetail_LadingPermit_LadingPermitId",
                        column: x => x.LadingPermitId,
                        principalSchema: "sepdb",
                        principalTable: "LadingPermit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingPermitDetail_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalSchema: "sepdb",
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingPermitDetail_PurchaseOrderDetails_PurchaseOrderDetailId",
                        column: x => x.PurchaseOrderDetailId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrderDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnloadingPermitDetails",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnloadingPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransferRemittanceDetailId = table.Column<int>(type: "int", nullable: false),
                    UnloadedAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnloadingPermitDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnloadingPermitDetails_TransferRemittanceDetails_TransferRemittanceDetailId",
                        column: x => x.TransferRemittanceDetailId,
                        principalSchema: "sepdb",
                        principalTable: "TransferRemittanceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnloadingPermitDetails_UnloadingPermits_UnloadingPermitId",
                        column: x => x.UnloadingPermitId,
                        principalSchema: "sepdb",
                        principalTable: "UnloadingPermits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DriverFareAmountApproves",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurOrderTransRemittUnloadingPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LadingExitPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverFareAmountApproves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverFareAmountApproves_LadingExitPermit_LadingExitPermitId",
                        column: x => x.LadingExitPermitId,
                        principalSchema: "sepdb",
                        principalTable: "LadingExitPermit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DriverFareAmountApproves_UnloadingPermits_PurOrderTransRemittUnloadingPermitId",
                        column: x => x.PurOrderTransRemittUnloadingPermitId,
                        principalSchema: "sepdb",
                        principalTable: "UnloadingPermits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DriverFareAmountApproves_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LadingExitPermitDetails",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LadingExitPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CargoAnnounceDetailId = table.Column<int>(type: "int", nullable: false),
                    RealAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    ProductSubUnitId = table.Column<int>(type: "int", nullable: false),
                    ProductSubUnitAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadingExitPermitDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LadingExitPermitDetails_CargoAnnounceDetails_CargoAnnounceDetailId",
                        column: x => x.CargoAnnounceDetailId,
                        principalSchema: "sepdb",
                        principalTable: "CargoAnnounceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingExitPermitDetails_LadingExitPermit_LadingExitPermitId",
                        column: x => x.LadingExitPermitId,
                        principalSchema: "sepdb",
                        principalTable: "LadingExitPermit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingExitPermitDetails_ProductUnits_ProductSubUnitId",
                        column: x => x.ProductSubUnitId,
                        principalSchema: "sepdb",
                        principalTable: "ProductUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingExitPermitDetails_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LadingLicense",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CargoAnnounceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HasExitPermit = table.Column<bool>(type: "bit", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FareAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    BankAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreditCardNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: true),
                    ExitPermitDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductSubUnitId = table.Column<int>(type: "int", nullable: true),
                    LadingExitPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadingLicense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LadingLicense_CargoAnnounces_CargoAnnounceId",
                        column: x => x.CargoAnnounceId,
                        principalSchema: "sepdb",
                        principalTable: "CargoAnnounces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingLicense_LadingExitPermit_LadingExitPermitId",
                        column: x => x.LadingExitPermitId,
                        principalSchema: "sepdb",
                        principalTable: "LadingExitPermit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LadingLicense_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LadingLicense_ProductUnits_ProductSubUnitId",
                        column: x => x.ProductSubUnitId,
                        principalSchema: "sepdb",
                        principalTable: "ProductUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LadingLicense_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RentPayments",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivePaymentOriginId = table.Column<int>(type: "int", nullable: false),
                    UnloadingPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LadingExitPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TotalFareAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentPayments_LadingExitPermit_LadingExitPermitId",
                        column: x => x.LadingExitPermitId,
                        principalSchema: "sepdb",
                        principalTable: "LadingExitPermit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentPayments_ReceivePaymentTypes_ReceivePaymentOriginId",
                        column: x => x.ReceivePaymentOriginId,
                        principalSchema: "sepdb",
                        principalTable: "ReceivePaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentPayments_UnloadingPermits_UnloadingPermitId",
                        column: x => x.UnloadingPermitId,
                        principalSchema: "sepdb",
                        principalTable: "UnloadingPermits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RentPayments_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ReceivePayId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurchaseOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LadingPermitId = table.Column<int>(type: "int", nullable: true),
                    PurOrderTransRemittanceUnloadingPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PurOrderTransRemittanceEntrancePermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LadingExitPermitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AttachmentType = table.Column<int>(type: "int", nullable: true),
                    CargoAnnounceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LadingLicenseId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_CargoAnnounces_CargoAnnounceId",
                        column: x => x.CargoAnnounceId,
                        principalSchema: "sepdb",
                        principalTable: "CargoAnnounces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_EntrancePermits_PurOrderTransRemittanceEntrancePermitId",
                        column: x => x.PurOrderTransRemittanceEntrancePermitId,
                        principalSchema: "sepdb",
                        principalTable: "EntrancePermits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_LadingExitPermit_LadingExitPermitId",
                        column: x => x.LadingExitPermitId,
                        principalSchema: "sepdb",
                        principalTable: "LadingExitPermit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_LadingLicense_LadingLicenseId",
                        column: x => x.LadingLicenseId,
                        principalSchema: "sepdb",
                        principalTable: "LadingLicense",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_LadingPermit_LadingPermitId",
                        column: x => x.LadingPermitId,
                        principalSchema: "sepdb",
                        principalTable: "LadingPermit",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalSchema: "sepdb",
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_ReceivePays_ReceivePayId",
                        column: x => x.ReceivePayId,
                        principalSchema: "sepdb",
                        principalTable: "ReceivePays",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attachment_UnloadingPermits_PurOrderTransRemittanceUnloadingPermitId",
                        column: x => x.PurOrderTransRemittanceUnloadingPermitId,
                        principalSchema: "sepdb",
                        principalTable: "UnloadingPermits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LadingLicenseDetail",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LadingLicenseId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    LadingAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    RealAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    PackageCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LadingLicenseDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LadingLicenseDetail_LadingLicense_LadingLicenseId",
                        column: x => x.LadingLicenseId,
                        principalSchema: "sepdb",
                        principalTable: "LadingLicense",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LadingLicenseDetail_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalSchema: "sepdb",
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationMenus_ApplicationMenuId",
                schema: "sepdb",
                table: "ApplicationMenus",
                column: "ApplicationMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationMenus_CreatedBy",
                schema: "sepdb",
                table: "ApplicationMenus",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_CargoAnnounceId",
                schema: "sepdb",
                table: "Attachment",
                column: "CargoAnnounceId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_LadingExitPermitId",
                schema: "sepdb",
                table: "Attachment",
                column: "LadingExitPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_LadingLicenseId",
                schema: "sepdb",
                table: "Attachment",
                column: "LadingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_LadingPermitId",
                schema: "sepdb",
                table: "Attachment",
                column: "LadingPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_OrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_PurchaseOrderId",
                schema: "sepdb",
                table: "Attachment",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_PurOrderTransRemittanceEntrancePermitId",
                schema: "sepdb",
                table: "Attachment",
                column: "PurOrderTransRemittanceEntrancePermitId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_PurOrderTransRemittanceUnloadingPermitId",
                schema: "sepdb",
                table: "Attachment",
                column: "PurOrderTransRemittanceUnloadingPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ReceivePayId",
                schema: "sepdb",
                table: "Attachment",
                column: "ReceivePayId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CreatedBy",
                schema: "sepdb",
                table: "AuditLogs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CargoAnnounceDetails_CargoAnnounceId",
                schema: "sepdb",
                table: "CargoAnnounceDetails",
                column: "CargoAnnounceId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoAnnounceDetails_CreatedBy",
                schema: "sepdb",
                table: "CargoAnnounceDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CargoAnnounceDetails_OrderDetailId",
                schema: "sepdb",
                table: "CargoAnnounceDetails",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoAnnounces_CreatedBy",
                schema: "sepdb",
                table: "CargoAnnounces",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CargoAnnounces_OrderId",
                schema: "sepdb",
                table: "CargoAnnounces",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoAnnounces_PurchaseOrderId",
                schema: "sepdb",
                table: "CargoAnnounces",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoAnnounces_VehicleTypeId",
                schema: "sepdb",
                table: "CargoAnnounces",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOfficialCompanies_CreatedBy",
                schema: "sepdb",
                table: "CustomerOfficialCompanies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOfficialCompanies_CustomerId",
                schema: "sepdb",
                table: "CustomerOfficialCompanies",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedBy",
                schema: "sepdb",
                table: "Customers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerValidityId",
                schema: "sepdb",
                table: "Customers",
                column: "CustomerValidityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWarehouses_CustomerId",
                schema: "sepdb",
                table: "CustomerWarehouses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWarehouses_WarehouseId",
                schema: "sepdb",
                table: "CustomerWarehouses",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverFareAmountApproves_CreatedBy",
                schema: "sepdb",
                table: "DriverFareAmountApproves",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_DriverFareAmountApproves_LadingExitPermitId",
                schema: "sepdb",
                table: "DriverFareAmountApproves",
                column: "LadingExitPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverFareAmountApproves_PurOrderTransRemittUnloadingPermitId",
                schema: "sepdb",
                table: "DriverFareAmountApproves",
                column: "PurOrderTransRemittUnloadingPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_EntrancePermits_CreatedBy",
                schema: "sepdb",
                table: "EntrancePermits",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EntrancePermits_TransferRemittanceId",
                schema: "sepdb",
                table: "EntrancePermits",
                column: "TransferRemittanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermit_CreatedBy",
                schema: "sepdb",
                table: "LadingExitPermit",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermit_LadingPermitId",
                schema: "sepdb",
                table: "LadingExitPermit",
                column: "LadingPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermitDetails_CargoAnnounceDetailId",
                schema: "sepdb",
                table: "LadingExitPermitDetails",
                column: "CargoAnnounceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermitDetails_CreatedBy",
                schema: "sepdb",
                table: "LadingExitPermitDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermitDetails_LadingExitPermitId",
                schema: "sepdb",
                table: "LadingExitPermitDetails",
                column: "LadingExitPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingExitPermitDetails_ProductSubUnitId",
                schema: "sepdb",
                table: "LadingExitPermitDetails",
                column: "ProductSubUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicense_CargoAnnounceId",
                schema: "sepdb",
                table: "LadingLicense",
                column: "CargoAnnounceId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicense_CreatedBy",
                schema: "sepdb",
                table: "LadingLicense",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicense_LadingExitPermitId",
                schema: "sepdb",
                table: "LadingLicense",
                column: "LadingExitPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicense_OrderId",
                schema: "sepdb",
                table: "LadingLicense",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicense_ProductSubUnitId",
                schema: "sepdb",
                table: "LadingLicense",
                column: "ProductSubUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicenseDetail_LadingLicenseId",
                schema: "sepdb",
                table: "LadingLicenseDetail",
                column: "LadingLicenseId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingLicenseDetail_OrderDetailId",
                schema: "sepdb",
                table: "LadingLicenseDetail",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingPermit_CargoAnnounceId_IsActive",
                schema: "sepdb",
                table: "LadingPermit",
                columns: new[] { "CargoAnnounceId", "IsActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LadingPermit_CreatedBy",
                schema: "sepdb",
                table: "LadingPermit",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LadingPermit_OrderId",
                schema: "sepdb",
                table: "LadingPermit",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingPermit_PurchaseOrderId",
                schema: "sepdb",
                table: "LadingPermit",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingPermitDetail_LadingPermitId",
                schema: "sepdb",
                table: "LadingPermitDetail",
                column: "LadingPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingPermitDetail_OrderDetailId",
                schema: "sepdb",
                table: "LadingPermitDetail",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_LadingPermitDetail_PurchaseOrderDetailId",
                schema: "sepdb",
                table: "LadingPermitDetail",
                column: "PurchaseOrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialWarehoseInventories_CreatedBy",
                schema: "sepdb",
                table: "OfficialWarehoseInventories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialWarehoseInventories_ProductId",
                schema: "sepdb",
                table: "OfficialWarehoseInventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialWarehoseInventories_WarehouseId",
                schema: "sepdb",
                table: "OfficialWarehoseInventories",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_AlternativeProductBrandId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "AlternativeProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductBrandId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductSubUnitId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "ProductSubUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PurchaseInvoiceTypeId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "PurchaseInvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PurchaseOrderId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PurchaserCustomerId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "PurchaserCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_WarehouseId",
                schema: "sepdb",
                table: "OrderDetails",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayments_OrderId",
                schema: "sepdb",
                table: "OrderPayments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedBy",
                schema: "sepdb",
                table: "Orders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                schema: "sepdb",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerOfficialCompanyId",
                schema: "sepdb",
                table: "Orders",
                column: "CustomerOfficialCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FarePaymentTypeId",
                schema: "sepdb",
                table: "Orders",
                column: "FarePaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_InvoiceTypeId",
                schema: "sepdb",
                table: "Orders",
                column: "InvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderExitTypeId",
                schema: "sepdb",
                table: "Orders",
                column: "OrderExitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderSendTypeId",
                schema: "sepdb",
                table: "Orders",
                column: "OrderSendTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                schema: "sepdb",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_OrderId",
                schema: "sepdb",
                table: "OrderServices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_ServiceId",
                schema: "sepdb",
                table: "OrderServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationBanks_BankId",
                schema: "sepdb",
                table: "OrganizationBanks",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ApplicationMenuId",
                schema: "sepdb",
                table: "Permissions",
                column: "ApplicationMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Phonebook_CreatedBy",
                schema: "sepdb",
                table: "Phonebook",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Phonebook_CustomerId",
                schema: "sepdb",
                table: "Phonebook",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Phonebook_PhoneNumberTypeId",
                schema: "sepdb",
                table: "Phonebook",
                column: "PhoneNumberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBrands_BrandId",
                schema: "sepdb",
                table: "ProductBrands",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBrands_ProductId",
                schema: "sepdb",
                table: "ProductBrands",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                schema: "sepdb",
                table: "ProductDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_CreatedBy",
                schema: "sepdb",
                table: "ProductInventories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_ProductBrandId",
                schema: "sepdb",
                table: "ProductInventories",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_ProductId",
                schema: "sepdb",
                table: "ProductInventories",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_WarehouseId",
                schema: "sepdb",
                table: "ProductInventories",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventoryHistories_CreatedBy",
                schema: "sepdb",
                table: "ProductInventoryHistories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_CreatedBy",
                schema: "sepdb",
                table: "ProductPrices",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductBrandId",
                schema: "sepdb",
                table: "ProductPrices",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                schema: "sepdb",
                table: "ProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedBy",
                schema: "sepdb",
                table: "Products",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCode",
                schema: "sepdb",
                table: "Products",
                column: "ProductCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductMainUnitId",
                schema: "sepdb",
                table: "Products",
                column: "ProductMainUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductStandardId",
                schema: "sepdb",
                table: "Products",
                column: "ProductStandardId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductStateId",
                schema: "sepdb",
                table: "Products",
                column: "ProductStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductSubUnitId",
                schema: "sepdb",
                table: "Products",
                column: "ProductSubUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                schema: "sepdb",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_WarehouseId",
                schema: "sepdb",
                table: "Products",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSuppliers_CreatedBy",
                schema: "sepdb",
                table: "ProductSuppliers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSuppliers_CustomerId",
                schema: "sepdb",
                table: "ProductSuppliers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSuppliers_ProductId",
                schema: "sepdb",
                table: "ProductSuppliers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_CustomerId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_CustomerOfficialCompanyId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "CustomerOfficialCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_DestinationWarehouseId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_FarePaymentTypeId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "FarePaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_InvoiceTypeId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "InvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_OrderSendTypeId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "OrderSendTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_OrderStatusId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_OriginWarehouseId",
                schema: "sepdb",
                table: "PurchaseOrder",
                column: "OriginWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_AlternativeProductBrandId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "AlternativeProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_OrderId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_ProductBrandId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_ProductSubUnitId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "ProductSubUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PurchaseInvoiceTypeId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "PurchaseInvoiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PurchaserCustomerId",
                schema: "sepdb",
                table: "PurchaseOrderDetails",
                column: "PurchaserCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderPayments_OrderId",
                schema: "sepdb",
                table: "PurchaseOrderPayments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderServices_OrderId",
                schema: "sepdb",
                table: "PurchaseOrderServices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderServices_ServiceId",
                schema: "sepdb",
                table: "PurchaseOrderServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderTransferDetails_PurchaseOrderDetailId",
                schema: "sepdb",
                table: "PurchaseOrderTransferDetails",
                column: "PurchaseOrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderTransferDetails_PurchaseOrderTransferId",
                schema: "sepdb",
                table: "PurchaseOrderTransferDetails",
                column: "PurchaseOrderTransferId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderTransfers_CreatedBy",
                schema: "sepdb",
                table: "PurchaseOrderTransfers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderTransfers_PurchaseOrderId",
                schema: "sepdb",
                table: "PurchaseOrderTransfers",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_CreatedBy",
                schema: "sepdb",
                table: "ReceivePays",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_PayToCashDeskId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "PayToCashDeskId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_PayToCompanyId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "PayToCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_PayToCostId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "PayToCostId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_PayToCustomerId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "PayToCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_PayToIncomeId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "PayToIncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_PayToOrganizationBankId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "PayToOrganizationBankId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_PayToPettyCashId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "PayToPettyCashId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_PayToShareHolderId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "PayToShareHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceiveFromCashDeskId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceiveFromCashDeskId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceiveFromCompanyId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceiveFromCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceiveFromCostId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceiveFromCostId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceiveFromCustomerId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceiveFromCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceiveFromIncomeId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceiveFromIncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceiveFromOrganizationBankId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceiveFromOrganizationBankId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceiveFromPettyCashId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceiveFromPettyCashId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceiveFromShareHolderId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceiveFromShareHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceivePaymentTypeFromId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceivePaymentTypeFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceivePaymentTypeToId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceivePaymentTypeToId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivePays_ReceivePayStatusId",
                schema: "sepdb",
                table: "ReceivePays",
                column: "ReceivePayStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_ApplicationUserId",
                schema: "sepdb",
                table: "RefreshToken",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_CreatedBy",
                schema: "sepdb",
                table: "RentPayments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_LadingExitPermitId",
                schema: "sepdb",
                table: "RentPayments",
                column: "LadingExitPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_ReceivePaymentOriginId",
                schema: "sepdb",
                table: "RentPayments",
                column: "ReceivePaymentOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_UnloadingPermitId",
                schema: "sepdb",
                table: "RentPayments",
                column: "UnloadingPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_ApplicationMenuId",
                schema: "sepdb",
                table: "RoleMenus",
                column: "ApplicationMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_ApplicationRoleId",
                schema: "sepdb",
                table: "RoleMenus",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "sepdb",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "sepdb",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareHolders_CreatedBy",
                schema: "sepdb",
                table: "ShareHolders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRemittanceDetails_ProductBrandId",
                schema: "sepdb",
                table: "TransferRemittanceDetails",
                column: "ProductBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRemittanceDetails_TransferRemittanceId",
                schema: "sepdb",
                table: "TransferRemittanceDetails",
                column: "TransferRemittanceId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRemittances_CreatedBy",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRemittances_DestinationWarehouseId",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "DestinationWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRemittances_OriginWarehouseId",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "OriginWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRemittances_TransferRemittanceStatusId",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "TransferRemittanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRemittances_TransferRemittanceTypeId",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "TransferRemittanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRemittances_VehicleTypeId",
                schema: "sepdb",
                table: "TransferRemittances",
                column: "VehicleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingPermitDetails_TransferRemittanceDetailId",
                schema: "sepdb",
                table: "UnloadingPermitDetails",
                column: "TransferRemittanceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingPermitDetails_UnloadingPermitId",
                schema: "sepdb",
                table: "UnloadingPermitDetails",
                column: "UnloadingPermitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingPermits_CreatedBy",
                schema: "sepdb",
                table: "UnloadingPermits",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingPermits_EntrancePermitId",
                schema: "sepdb",
                table: "UnloadingPermits",
                column: "EntrancePermitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingPermits_TransferRemittanceDetailId",
                schema: "sepdb",
                table: "UnloadingPermits",
                column: "TransferRemittanceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "sepdb",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "sepdb",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_WarehouseTypeId",
                schema: "sepdb",
                table: "Warehouses",
                column: "WarehouseTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "CustomerWarehouses",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "DriverFareAmountApproves",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "LadingExitPermitDetails",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "LadingLicenseDetail",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "LadingPermitDetail",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OfficialWarehoseInventories",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderPayments",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderServices",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Phonebook",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductDetails",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductInventories",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductInventoryHistories",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductPrices",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductSuppliers",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseOrderPayments",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseOrderServices",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseOrderTransferDetails",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "RentPayments",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "RoleMenus",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "UnloadingPermitDetails",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ReceivePays",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "CargoAnnounceDetails",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "LadingLicense",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PhoneNumberTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseOrderTransfers",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "UnloadingPermits",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "CashDesks",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Costs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Incomes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrganizationBanks",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PettyCashs",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ReceivePayStatus",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ReceivePaymentTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ShareHolders",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderDetails",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "LadingExitPermit",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ApplicationMenus",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "EntrancePermits",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "TransferRemittanceDetails",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Banks",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseInvoiceTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "LadingPermit",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductBrands",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "TransferRemittances",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "CargoAnnounces",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Brands",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "TransferRemittanceStatus",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "TransferRemittanceTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseOrder",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "VehicleTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductStandards",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductStates",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "ProductUnits",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderExitTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderSendTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderStatuses",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PaymentTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "CustomerOfficialCompanies",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "InvoiceTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseOrderFarePaymentTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseOrderSendTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "PurchaseOrderStatus",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Warehouses",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "WarehouseTypes",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "CustomerValidities",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "sepdb");
        }
    }
}
