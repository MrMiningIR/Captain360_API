using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capitan360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class indexc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyPackageTypes_CompanyId",
                table: "CompanyPackageTypes");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContentTypes_CompanyId_ContentTypeId",
                table: "CompanyContentTypes");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPackageType_Active",
                table: "CompanyPackageTypes",
                columns: new[] { "CompanyId", "PackageTypeId" },
                unique: true,
                filter: "[Deleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContentType_Active",
                table: "CompanyContentTypes",
                columns: new[] { "CompanyId", "ContentTypeId" },
                unique: true,
                filter: "[Deleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CompanyPackageType_Active",
                table: "CompanyPackageTypes");

            migrationBuilder.DropIndex(
                name: "IX_CompanyContentType_Active",
                table: "CompanyContentTypes");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPackageTypes_CompanyId",
                table: "CompanyPackageTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContentTypes_CompanyId_ContentTypeId",
                table: "CompanyContentTypes",
                columns: new[] { "CompanyId", "ContentTypeId" },
                unique: true);
        }
    }
}
