using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408051151pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabel_Customers_CustomerId",
                schema: "sepdb",
                table: "CustomerLabel");

            migrationBuilder.DropIndex(
                name: "IX_CustomerLabel_CustomerId",
                schema: "sepdb",
                table: "CustomerLabel");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "sepdb",
                table: "CustomerLabel");

            migrationBuilder.CreateTable(
                name: "CustomerAssignedLabel",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerLabelId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAssignedLabel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAssignedLabel_CustomerLabel_CustomerLabelId",
                        column: x => x.CustomerLabelId,
                        principalSchema: "sepdb",
                        principalTable: "CustomerLabel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAssignedLabel_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "sepdb",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAssignedLabel_CustomerId",
                schema: "sepdb",
                table: "CustomerAssignedLabel",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAssignedLabel_CustomerLabelId",
                schema: "sepdb",
                table: "CustomerAssignedLabel",
                column: "CustomerLabelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerAssignedLabel",
                schema: "sepdb");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "sepdb",
                table: "CustomerLabel",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLabel_CustomerId",
                schema: "sepdb",
                table: "CustomerLabel",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabel_Customers_CustomerId",
                schema: "sepdb",
                table: "CustomerLabel",
                column: "CustomerId",
                principalSchema: "sepdb",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
