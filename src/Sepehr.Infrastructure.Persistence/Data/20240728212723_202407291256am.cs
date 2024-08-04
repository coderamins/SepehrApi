using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202407291256am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Phonebook_CustomerId_PhoneNumberTypeId",
                schema: "sepdb",
                table: "Phonebook");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "sepdb",
                table: "Phonebook",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Phonebook_CustomerId_PhoneNumber_PhoneNumberTypeId",
                schema: "sepdb",
                table: "Phonebook",
                columns: new[] { "CustomerId", "PhoneNumber", "PhoneNumberTypeId" },
                unique: true,
                filter: "[CustomerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Phonebook_CustomerId_PhoneNumber_PhoneNumberTypeId",
                schema: "sepdb",
                table: "Phonebook");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "sepdb",
                table: "Phonebook",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Phonebook_CustomerId_PhoneNumberTypeId",
                schema: "sepdb",
                table: "Phonebook",
                columns: new[] { "CustomerId", "PhoneNumberTypeId" },
                unique: true,
                filter: "[CustomerId] IS NOT NULL");
        }
    }
}
