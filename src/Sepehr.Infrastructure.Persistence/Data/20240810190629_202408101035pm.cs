using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408101035pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnloadingPermitCode",
                schema: "sepdb",
                table: "UnloadingPermits");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnloadingPermitCode",
                schema: "sepdb",
                table: "UnloadingPermits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
