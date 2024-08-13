using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408130252pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PersonnelCode",
                schema: "sepdb",
                table: "Personnels",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1000, 1");

            migrationBuilder.AddColumn<long>(
                name: "CustomerCode",
                schema: "sepdb",
                table: "Customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "100, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonnelCode",
                schema: "sepdb",
                table: "Personnels");

            migrationBuilder.DropColumn(
                name: "CustomerCode",
                schema: "sepdb",
                table: "Customers");
        }
    }
}
