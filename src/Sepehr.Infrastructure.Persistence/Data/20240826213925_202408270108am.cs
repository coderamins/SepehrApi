using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202408270108am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerValidities_CustomerValidityId",
                schema: "sepdb",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerValidities",
                schema: "sepdb",
                table: "CustomerValidities");

            migrationBuilder.RenameTable(
                name: "CustomerValidities",
                schema: "sepdb",
                newName: "CustomerValidity",
                newSchema: "sepdb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerValidity",
                schema: "sepdb",
                table: "CustomerValidity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InternalPhoneNumber",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhonebookId = table.Column<int>(type: "int", nullable: false),
                    InternalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalPhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalPhoneNumber_Phonebook_PhonebookId",
                        column: x => x.PhonebookId,
                        principalSchema: "sepdb",
                        principalTable: "Phonebook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalPhoneNumber_PhonebookId",
                schema: "sepdb",
                table: "InternalPhoneNumber",
                column: "PhonebookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerValidity_CustomerValidityId",
                schema: "sepdb",
                table: "Customers",
                column: "CustomerValidityId",
                principalSchema: "sepdb",
                principalTable: "CustomerValidity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerValidity_CustomerValidityId",
                schema: "sepdb",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "InternalPhoneNumber",
                schema: "sepdb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerValidity",
                schema: "sepdb",
                table: "CustomerValidity");

            migrationBuilder.RenameTable(
                name: "CustomerValidity",
                schema: "sepdb",
                newName: "CustomerValidities",
                newSchema: "sepdb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerValidities",
                schema: "sepdb",
                table: "CustomerValidities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerValidities_CustomerValidityId",
                schema: "sepdb",
                table: "Customers",
                column: "CustomerValidityId",
                principalSchema: "sepdb",
                principalTable: "CustomerValidities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
