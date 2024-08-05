using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408060156am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerLabelTypes",
                schema: "sepdb",
                table: "CustomerLabelTypes");

            migrationBuilder.DropColumn(
                name: "CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabelTypes");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                schema: "sepdb",
                table: "CustomerLabelTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "sepdb",
                table: "CustomerLabelTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerLabelTypes",
                schema: "sepdb",
                table: "CustomerLabelTypes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerLabelTypes",
                schema: "sepdb",
                table: "CustomerLabelTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "sepdb",
                table: "CustomerLabelTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "sepdb",
                table: "CustomerLabelTypes");

            migrationBuilder.AddColumn<int>(
                name: "CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabelTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerLabelTypes",
                schema: "sepdb",
                table: "CustomerLabelTypes",
                column: "CustomerLabelTypeId");
        }
    }
}
