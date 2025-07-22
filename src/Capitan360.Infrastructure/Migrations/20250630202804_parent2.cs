using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capitan360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class parent2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionScope",
                table: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "ParentDisplayName",
                table: "Permissions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentDisplayName",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionScope",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
