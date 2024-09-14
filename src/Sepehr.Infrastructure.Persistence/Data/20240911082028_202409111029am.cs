using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class _202409111029am : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrimaryOrders",
                schema: "sepdb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Converted = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrimaryOrders_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "sepdb",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrimaryOrders_CreatedBy",
                schema: "sepdb",
                table: "PrimaryOrders",
                column: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrimaryOrders",
                schema: "sepdb");
        }
    }
}
