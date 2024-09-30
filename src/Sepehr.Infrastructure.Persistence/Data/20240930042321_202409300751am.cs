using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409300751am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoAnnounces_VehicleTypes_VehicleTypeId",
                schema: "sepdb",
                table: "CargoAnnounces");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleTypeId",
                schema: "sepdb",
                table: "CargoAnnounces",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoAnnounces_VehicleTypes_VehicleTypeId",
                schema: "sepdb",
                table: "CargoAnnounces",
                column: "VehicleTypeId",
                principalSchema: "sepdb",
                principalTable: "VehicleTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoAnnounces_VehicleTypes_VehicleTypeId",
                schema: "sepdb",
                table: "CargoAnnounces");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleTypeId",
                schema: "sepdb",
                table: "CargoAnnounces",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CargoAnnounces_VehicleTypes_VehicleTypeId",
                schema: "sepdb",
                table: "CargoAnnounces",
                column: "VehicleTypeId",
                principalSchema: "sepdb",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
