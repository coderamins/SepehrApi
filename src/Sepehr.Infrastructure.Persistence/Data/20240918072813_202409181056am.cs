using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409181056am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleMenus_Roles_ApplicationRoleId",
                schema: "sepdb",
                table: "RoleMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                schema: "sepdb",
                table: "RolePermissions");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMenus_Roles_ApplicationRoleId",
                schema: "sepdb",
                table: "RoleMenus",
                column: "ApplicationRoleId",
                principalSchema: "sepdb",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                schema: "sepdb",
                table: "RolePermissions",
                column: "RoleId",
                principalSchema: "sepdb",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleMenus_Roles_ApplicationRoleId",
                schema: "sepdb",
                table: "RoleMenus");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                schema: "sepdb",
                table: "RolePermissions");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMenus_Roles_ApplicationRoleId",
                schema: "sepdb",
                table: "RoleMenus",
                column: "ApplicationRoleId",
                principalSchema: "sepdb",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                schema: "sepdb",
                table: "RolePermissions",
                column: "RoleId",
                principalSchema: "sepdb",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
