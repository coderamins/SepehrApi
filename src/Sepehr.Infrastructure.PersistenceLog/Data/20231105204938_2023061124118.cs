using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.PersistenceLog.Data
{
    /// <inheritdoc />
    public partial class _2023061124118 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainId",
                schema: "seplog",
                table: "ProductDetailLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainId",
                schema: "seplog",
                table: "ProductDetailLogs");
        }
    }
}
