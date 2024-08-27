using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408270111am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerValidity_CustomerValidityId",
                schema: "sepdb",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerValidity",
                schema: "sepdb");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerValidityId",
                schema: "sepdb",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerValidity",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ValidityDesc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerValidity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerValidityId",
                schema: "sepdb",
                table: "Customers",
                column: "CustomerValidityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerValidity_CustomerValidityId",
                schema: "sepdb",
                table: "Customers",
                column: "CustomerValidityId",
                principalSchema: "sepdb",
                principalTable: "CustomerValidity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
