using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capitan360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class uvc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissionVersionControls",
                table: "UserPermissionVersionControls");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissionVersionControls",
                table: "UserPermissionVersionControls",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionVersionControl_Active",
                table: "UserPermissionVersionControls",
                columns: new[] { "UserId", "PermissionVersion" },
                unique: true,
                filter: "[Deleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissionVersionControls",
                table: "UserPermissionVersionControls");

            migrationBuilder.DropIndex(
                name: "IX_UserPermissionVersionControl_Active",
                table: "UserPermissionVersionControls");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissionVersionControls",
                table: "UserPermissionVersionControls",
                columns: new[] { "UserId", "Id", "PermissionVersion" });
        }
    }
}
