using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408071116pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessCode",
                schema: "sepdb",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                schema: "sepdb",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessCode",
                schema: "sepdb",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "NickName",
                schema: "sepdb",
                table: "Customers");
        }
    }
}
