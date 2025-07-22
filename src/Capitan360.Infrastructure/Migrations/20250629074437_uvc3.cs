using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capitan360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class uvc3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ۱. حذف کلید اصلی فعلی
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissionVersionControls",
                table: "UserPermissionVersionControls");



            // ۳. حذف ستون Id
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPermissionVersionControls");

            // ۴. ایجاد ستون جدید Id با خاصیت IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserPermissionVersionControls",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");





            // ۷. افزودن کلید اصلی جدید
            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissionVersionControls",
                table: "UserPermissionVersionControls",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ۱. حذف کلید اصلی
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPermissionVersionControls",
                table: "UserPermissionVersionControls");



            // ۳. حذف ستون Id با خاصیت IDENTITY
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPermissionVersionControls");

            // ۴. ایجاد ستون Id بدون خاصیت IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserPermissionVersionControls",
                type: "int",
                nullable: false);



            // ۷. افزودن کلید اصلی ترکیبی (مانند حالت قبلی)
            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPermissionVersionControls",
                table: "UserPermissionVersionControls",
                columns: new[] { "UserId", "Id" });
        }
    }
}
