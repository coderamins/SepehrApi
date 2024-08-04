using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202407311031pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerLabelType",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelTypeDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLabelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLabel",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerLabelTypeId = table.Column<int>(type: "int", nullable: false),
                    LabelNameCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LabelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLabel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLabel_CustomerLabelType_CustomerLabelTypeId",
                        column: x => x.CustomerLabelTypeId,
                        principalSchema: "sepdb",
                        principalTable: "CustomerLabelType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLabel_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerLabel_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLabel_CreatedBy",
                schema: "sepdb",
                table: "CustomerLabel",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLabel_CustomerId",
                schema: "sepdb",
                table: "CustomerLabel",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLabel_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabel",
                column: "CustomerLabelTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerLabel",
                schema: "sepdb");

            migrationBuilder.DropTable(
                name: "CustomerLabelType",
                schema: "sepdb");
        }
    }
}
