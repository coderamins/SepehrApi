using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408051216pm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAssignedLabel_CustomerLabel_CustomerLabelId",
                schema: "sepdb",
                table: "CustomerAssignedLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAssignedLabel_Customers_CustomerId",
                schema: "sepdb",
                table: "CustomerAssignedLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabel_CustomerLabelType_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabel");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabel_Users_CreatedBy",
                schema: "sepdb",
                table: "CustomerLabel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerLabel",
                schema: "sepdb",
                table: "CustomerLabel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAssignedLabel",
                schema: "sepdb",
                table: "CustomerAssignedLabel");

            migrationBuilder.RenameTable(
                name: "CustomerLabel",
                schema: "sepdb",
                newName: "CustomerLabels",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "CustomerAssignedLabel",
                schema: "sepdb",
                newName: "CustomerAssignedLabels",
                newSchema: "sepdb");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerLabel_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels",
                newName: "IX_CustomerLabels_CustomerLabelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerLabel_CreatedBy",
                schema: "sepdb",
                table: "CustomerLabels",
                newName: "IX_CustomerLabels_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAssignedLabel_CustomerLabelId",
                schema: "sepdb",
                table: "CustomerAssignedLabels",
                newName: "IX_CustomerAssignedLabels_CustomerLabelId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAssignedLabel_CustomerId",
                schema: "sepdb",
                table: "CustomerAssignedLabels",
                newName: "IX_CustomerAssignedLabels_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerLabels",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAssignedLabels",
                schema: "sepdb",
                table: "CustomerAssignedLabels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAssignedLabels_CustomerLabels_CustomerLabelId",
                schema: "sepdb",
                table: "CustomerAssignedLabels",
                column: "CustomerLabelId",
                principalSchema: "sepdb",
                principalTable: "CustomerLabels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAssignedLabels_Customers_CustomerId",
                schema: "sepdb",
                table: "CustomerAssignedLabels",
                column: "CustomerId",
                principalSchema: "sepdb",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabels_CustomerLabelType_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "CustomerLabelTypeId",
                principalSchema: "sepdb",
                principalTable: "CustomerLabelType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabels_Users_CreatedBy",
                schema: "sepdb",
                table: "CustomerLabels",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAssignedLabels_CustomerLabels_CustomerLabelId",
                schema: "sepdb",
                table: "CustomerAssignedLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAssignedLabels_Customers_CustomerId",
                schema: "sepdb",
                table: "CustomerAssignedLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabels_CustomerLabelType_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerLabels_Users_CreatedBy",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerLabels",
                schema: "sepdb",
                table: "CustomerLabels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerAssignedLabels",
                schema: "sepdb",
                table: "CustomerAssignedLabels");

            migrationBuilder.RenameTable(
                name: "CustomerLabels",
                schema: "sepdb",
                newName: "CustomerLabel",
                newSchema: "sepdb");

            migrationBuilder.RenameTable(
                name: "CustomerAssignedLabels",
                schema: "sepdb",
                newName: "CustomerAssignedLabel",
                newSchema: "sepdb");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerLabels_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabel",
                newName: "IX_CustomerLabel_CustomerLabelTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerLabels_CreatedBy",
                schema: "sepdb",
                table: "CustomerLabel",
                newName: "IX_CustomerLabel_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAssignedLabels_CustomerLabelId",
                schema: "sepdb",
                table: "CustomerAssignedLabel",
                newName: "IX_CustomerAssignedLabel_CustomerLabelId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerAssignedLabels_CustomerId",
                schema: "sepdb",
                table: "CustomerAssignedLabel",
                newName: "IX_CustomerAssignedLabel_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerLabel",
                schema: "sepdb",
                table: "CustomerLabel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerAssignedLabel",
                schema: "sepdb",
                table: "CustomerAssignedLabel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAssignedLabel_CustomerLabel_CustomerLabelId",
                schema: "sepdb",
                table: "CustomerAssignedLabel",
                column: "CustomerLabelId",
                principalSchema: "sepdb",
                principalTable: "CustomerLabel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAssignedLabel_Customers_CustomerId",
                schema: "sepdb",
                table: "CustomerAssignedLabel",
                column: "CustomerId",
                principalSchema: "sepdb",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabel_CustomerLabelType_CustomerLabelTypeId",
                schema: "sepdb",
                table: "CustomerLabel",
                column: "CustomerLabelTypeId",
                principalSchema: "sepdb",
                principalTable: "CustomerLabelType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerLabel_Users_CreatedBy",
                schema: "sepdb",
                table: "CustomerLabel",
                column: "CreatedBy",
                principalSchema: "sepdb",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
