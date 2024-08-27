using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408270131am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerValidityId",
                schema: "sepdb",
                table: "Customers",
                column: "CustomerValidityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerValidities_CustomerValidityId",
                schema: "sepdb",
                table: "Customers",
                column: "CustomerValidityId",
                principalSchema: "sepdb",
                principalTable: "CustomerValidities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerValidities_CustomerValidityId",
                schema: "sepdb",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerValidityId",
                schema: "sepdb",
                table: "Customers");
        }
    }
}
