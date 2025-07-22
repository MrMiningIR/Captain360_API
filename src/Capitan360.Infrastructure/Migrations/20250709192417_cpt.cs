using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capitan360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cpt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageTypeDescription",
                table: "CompanyPackageTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackageTypeDescription",
                table: "CompanyPackageTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
