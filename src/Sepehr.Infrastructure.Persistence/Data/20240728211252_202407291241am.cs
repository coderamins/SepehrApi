using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202407291241am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Phonebook_CustomerId",
                schema: "sepdb",
                table: "Phonebook");

            migrationBuilder.CreateIndex(
                name: "IX_Phonebook_CustomerId_PhoneNumberTypeId",
                schema: "sepdb",
                table: "Phonebook",
                columns: new[] { "CustomerId", "PhoneNumberTypeId" },
                unique: true,
                filter: "[CustomerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Phonebook_CustomerId_PhoneNumberTypeId",
                schema: "sepdb",
                table: "Phonebook");

            migrationBuilder.CreateIndex(
                name: "IX_Phonebook_CustomerId",
                schema: "sepdb",
                table: "Phonebook",
                column: "CustomerId");
        }
    }
}
