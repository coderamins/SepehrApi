using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409140904pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                schema: "sepdb",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductTypeId",
                schema: "sepdb",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderReturn",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReturnStatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderReturn_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "sepdb",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderReturn_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetailReturn",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    OrderReturnId = table.Column<int>(type: "int", nullable: false),
                    ReturnedAmount = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetailReturn_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalSchema: "sepdb",
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetailReturn_OrderReturn_OrderReturnId",
                        column: x => x.OrderReturnId,
                        principalSchema: "sepdb",
                        principalTable: "OrderReturn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetailReturn_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailReturn_CreatedBy",
                schema: "sepdb",
                table: "OrderDetailReturn",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailReturn_OrderDetailId",
                schema: "sepdb",
                table: "OrderDetailReturn",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailReturn_OrderReturnId",
                schema: "sepdb",
                table: "OrderDetailReturn",
                column: "OrderReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderReturn_CreatedBy",
                schema: "sepdb",
                table: "OrderReturn",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_OrderReturn_OrderId",
                schema: "sepdb",
                table: "OrderReturn",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                schema: "sepdb",
                table: "Products",
                column: "ProductTypeId",
                principalSchema: "sepdb",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                schema: "sepdb",
                table: "Products");

            migrationBuilder.DropTable(
                name: "OrderDetailReturn",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "OrderReturn",
                schema: "sepdb");

            migrationBuilder.AlterColumn<int>(
                name: "ProductTypeId",
                schema: "sepdb",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                schema: "sepdb",
                table: "Products",
                column: "ProductTypeId",
                principalSchema: "sepdb",
                principalTable: "ProductTypes",
                principalColumn: "Id");
        }
    }
}
