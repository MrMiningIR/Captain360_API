using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capitan360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    LevelId = table.Column<short>(type: "smallint", nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Longitude = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_Areas_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CapitanCargoCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivationCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    ActivationCodeExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveSessionId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UserKind = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.CheckConstraint("CK_User_PhoneNumber_Length", "LEN(PhoneNumber) = 11");
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyTypeId = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Areas_CityId",
                        column: x => x.CityId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Areas_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Areas_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyTypes_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyTypeId = table.Column<int>(type: "int", nullable: false),
                    ContentTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ContentTypeDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderContentType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentTypes_CompanyTypes_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyTypeId = table.Column<int>(type: "int", nullable: false),
                    PackageTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PackageTypeDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderPackageType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageTypes_CompanyTypes_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupPermissions",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermissions", x => new { x.GroupId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_GroupPermissions_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GroupPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleGroups",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroups", x => new { x.RoleId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_RoleGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleGroups_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IV = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    ClientIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TokenBlacklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokenBlacklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokenBlacklists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissionVersionControls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionVersion = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissionVersionControls", x => new { x.UserId, x.Id, x.PermissionVersion });
                    table.ForeignKey(
                        name: "FK_UserPermissionVersionControls_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TelegramPhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    TellNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EconomicCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegistrationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    MoadianFactorType = table.Column<int>(type: "int", nullable: false),
                    IsBikeDelivery = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RecoveryPasswordCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecoveryPasswordCodeExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Credit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    HasCredit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Mobile = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    Tel1 = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    Tel2 = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    Zipcode = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Longitude = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OrderAddress = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyCommissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommissionFromCaptainCargoWebSite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionFromCompanyWebSite = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionFromCaptainCargoWebService = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionFromCompanyWebService = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionFromCaptainCargoPanel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionFromCaptainCargoDesktop = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCommissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCommissions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyDomesticPaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DescriptionForSearch = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EntranceFee = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    EntranceWeight = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    EntranceType = table.Column<int>(type: "int", nullable: false),
                    ExitStampBillMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitPackagingMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitAccumulationMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitExtraSourceMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitPricingMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitRevenue1MinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitRevenue2MinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitRevenue3MinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitDistributionMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitExtraDestinationMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    SourceCountryId = table.Column<int>(type: "int", nullable: false),
                    SourceProvinceId = table.Column<int>(type: "int", nullable: false),
                    SourceCityId = table.Column<int>(type: "int", nullable: false),
                    DestinationCountryId = table.Column<int>(type: "int", nullable: false),
                    DestinationProvinceId = table.Column<int>(type: "int", nullable: false),
                    DestinationCityId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDomesticPaths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPaths_Areas_DestinationCityId",
                        column: x => x.DestinationCityId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPaths_Areas_DestinationCountryId",
                        column: x => x.DestinationCountryId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPaths_Areas_DestinationProvinceId",
                        column: x => x.DestinationProvinceId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPaths_Areas_SourceCityId",
                        column: x => x.SourceCityId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPaths_Areas_SourceCountryId",
                        column: x => x.SourceCountryId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPaths_Areas_SourceProvinceId",
                        column: x => x.SourceProvinceId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPaths_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyInsurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Scale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CompanyTypeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInsurances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyInsurances_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyInsurances_CompanyTypes_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EconomicCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegistrationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CaptainCargoName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CaptainCargoCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ActiveIssueDomesticWaybill = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActiveShowInSearchEngine = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActiveInWebServiceSearchEngine = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ActiveInternationalAirlineCargo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitStampBillMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitPackagingMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitAccumulationMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitExtraSourceMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitPricingMinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitRevenue1MinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitRevenue2MinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitRevenue3MinWeightIsFixed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitFareInTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitStampBillInTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitPackagingInTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitAccumulationInTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitExtraSourceInTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitPricingInTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitRevenue1InTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitRevenue2InTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitRevenue3InTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitDistributionInTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExitExtraDestinationInTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPreferences_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanySmsPatterns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatternSmsIssueSender = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsIssueReceiver = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsIssueCompany = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsSendSenderPeakSender = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsSendSenderPeakReceiver = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsPackageInCompanySender = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsPackageInCompanyReceiver = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsManifestSender = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsManifestReceiver = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsReceivedInReceiverCompanySender = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsReceivedInReceiverCompanyReceiver = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsSendReceiverPeakSender = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsSendReceiverPeakReceiver = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsDeliverSender = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsDeliverReceiver = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsCancelSender = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsCancelReceiver = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsCancelByCustomerSender = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsCancelByCustomerReceiver = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsCancelByCustomerCompany = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatternSmsSendManifestReceiverCompany = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySmsPatterns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanySmsPatterns_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyUris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uri = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUris", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUris_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCompanies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserCompanies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ContentTypeId = table.Column<int>(type: "int", nullable: false),
                    ContentTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    OrderContentType = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContentTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyContentTypes_ContentTypes_ContentTypeId",
                        column: x => x.ContentTypeId,
                        principalTable: "ContentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyPackageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    PackageTypeId = table.Column<int>(type: "int", nullable: false),
                    PackageTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PackageTypeDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderPackageType = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPackageTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPackageTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyPackageTypes_PackageTypes_PackageTypeId",
                        column: x => x.PackageTypeId,
                        principalTable: "PackageTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyDomesticPathCharges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyDomesticPathId = table.Column<int>(type: "int", nullable: false),
                    ContentTypeId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    WeightType = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    ContentTypeChargeBaseNormal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDomesticPathCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPathCharges_CompanyDomesticPaths_CompanyDomesticPathId",
                        column: x => x.CompanyDomesticPathId,
                        principalTable: "CompanyDomesticPaths",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyDomesticPathStructPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyDomesticPathId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    PathStructType = table.Column<int>(type: "int", nullable: false),
                    WeightType = table.Column<int>(type: "int", nullable: false),
                    MunicipalAreaId = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDomesticPathStructPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPathStructPrices_CompanyDomesticPaths_CompanyDomesticPathId",
                        column: x => x.CompanyDomesticPathId,
                        principalTable: "CompanyDomesticPaths",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyInsuranceCharges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Settlement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPercent = table.Column<bool>(type: "bit", nullable: false),
                    Static = table.Column<bool>(type: "bit", nullable: false),
                    CompanyInsuranceId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInsuranceCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyInsuranceCharges_CompanyInsurances_CompanyInsuranceId",
                        column: x => x.CompanyInsuranceId,
                        principalTable: "CompanyInsurances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyDomesticPathChargeContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyDomesticPathChargeId = table.Column<int>(type: "int", nullable: false),
                    WeightType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    ContentTypeId = table.Column<int>(type: "int", nullable: false),
                    CompanyDomesticPathId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDomesticPathChargeContentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPathChargeContentTypes_CompanyDomesticPathCharges_CompanyDomesticPathChargeId",
                        column: x => x.CompanyDomesticPathChargeId,
                        principalTable: "CompanyDomesticPathCharges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPathChargeContentTypes_CompanyDomesticPaths_CompanyDomesticPathId",
                        column: x => x.CompanyDomesticPathId,
                        principalTable: "CompanyDomesticPaths",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPathChargeContentTypes_ContentTypes_ContentTypeId",
                        column: x => x.ContentTypeId,
                        principalTable: "ContentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDomesticPathStructPriceMunicipalAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyDomesticPathStructPriceId = table.Column<int>(type: "int", nullable: false),
                    MunicipalAreaId = table.Column<int>(type: "int", nullable: false),
                    WeightType = table.Column<int>(type: "int", nullable: false),
                    PathStructType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Static = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CompanyDomesticPathId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDomesticPathStructPriceMunicipalAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPathStructPriceMunicipalAreas_Areas_MunicipalAreaId",
                        column: x => x.MunicipalAreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPathStructPriceMunicipalAreas_CompanyDomesticPathStructPrices_CompanyDomesticPathStructPriceId",
                        column: x => x.CompanyDomesticPathStructPriceId,
                        principalTable: "CompanyDomesticPathStructPrices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyDomesticPathStructPriceMunicipalAreas_CompanyDomesticPaths_CompanyDomesticPathId",
                        column: x => x.CompanyDomesticPathId,
                        principalTable: "CompanyDomesticPaths",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyInsurancesChargePaymentContentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyInsuranceChargeId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    RateSettlement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPercentRateSettlement = table.Column<bool>(type: "bit", nullable: false),
                    RateDiff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPercentDiff = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInsurancesChargePaymentContentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyInsurancesChargePaymentContentTypes_CompanyInsuranceCharges_CompanyInsuranceChargeId",
                        column: x => x.CompanyInsuranceChargeId,
                        principalTable: "CompanyInsuranceCharges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyInsurancesChargePaymentContentTypes_ContentTypes_ContentId",
                        column: x => x.ContentId,
                        principalTable: "ContentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyInsurancesChargePayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyInsuranceChargeId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Diff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPercent = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInsurancesChargePayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyInsurancesChargePayments_CompanyInsuranceCharges_CompanyInsuranceChargeId",
                        column: x => x.CompanyInsuranceChargeId,
                        principalTable: "CompanyInsuranceCharges",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CompanyId",
                table: "Addresses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_ParentId",
                table: "Areas",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityId",
                table: "Companies",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyTypeId",
                table: "Companies",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                table: "Companies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ProvinceId",
                table: "Companies",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCommissions_CompanyId",
                table: "CompanyCommissions",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContentTypes_CompanyId_ContentTypeId",
                table: "CompanyContentTypes",
                columns: new[] { "CompanyId", "ContentTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContentTypes_ContentTypeId",
                table: "CompanyContentTypes",
                column: "ContentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPathChargeContentTypes_CompanyDomesticPathChargeId",
                table: "CompanyDomesticPathChargeContentTypes",
                column: "CompanyDomesticPathChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPathChargeContentTypes_CompanyDomesticPathId",
                table: "CompanyDomesticPathChargeContentTypes",
                column: "CompanyDomesticPathId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPathChargeContentTypes_ContentTypeId",
                table: "CompanyDomesticPathChargeContentTypes",
                column: "ContentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPathCharges_CompanyDomesticPathId",
                table: "CompanyDomesticPathCharges",
                column: "CompanyDomesticPathId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPaths_CompanyId",
                table: "CompanyDomesticPaths",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPaths_DestinationCityId",
                table: "CompanyDomesticPaths",
                column: "DestinationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPaths_DestinationCountryId",
                table: "CompanyDomesticPaths",
                column: "DestinationCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPaths_DestinationProvinceId",
                table: "CompanyDomesticPaths",
                column: "DestinationProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPaths_SourceCityId",
                table: "CompanyDomesticPaths",
                column: "SourceCityId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPaths_SourceCountryId",
                table: "CompanyDomesticPaths",
                column: "SourceCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPaths_SourceProvinceId",
                table: "CompanyDomesticPaths",
                column: "SourceProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPathStructPriceMunicipalAreas_CompanyDomesticPathId",
                table: "CompanyDomesticPathStructPriceMunicipalAreas",
                column: "CompanyDomesticPathId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPathStructPriceMunicipalAreas_CompanyDomesticPathStructPriceId",
                table: "CompanyDomesticPathStructPriceMunicipalAreas",
                column: "CompanyDomesticPathStructPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPathStructPriceMunicipalAreas_MunicipalAreaId",
                table: "CompanyDomesticPathStructPriceMunicipalAreas",
                column: "MunicipalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDomesticPathStructPrices_CompanyDomesticPathId",
                table: "CompanyDomesticPathStructPrices",
                column: "CompanyDomesticPathId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInsuranceCharges_CompanyInsuranceId",
                table: "CompanyInsuranceCharges",
                column: "CompanyInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInsurances_CompanyId",
                table: "CompanyInsurances",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInsurances_CompanyTypeId",
                table: "CompanyInsurances",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInsurancesChargePaymentContentTypes_CompanyInsuranceChargeId",
                table: "CompanyInsurancesChargePaymentContentTypes",
                column: "CompanyInsuranceChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInsurancesChargePaymentContentTypes_ContentId",
                table: "CompanyInsurancesChargePaymentContentTypes",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyInsurancesChargePayments_CompanyInsuranceChargeId",
                table: "CompanyInsurancesChargePayments",
                column: "CompanyInsuranceChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPackageTypes_CompanyId",
                table: "CompanyPackageTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPackageTypes_PackageTypeId",
                table: "CompanyPackageTypes",
                column: "PackageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPreferences_CompanyId",
                table: "CompanyPreferences",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanySmsPatterns_CompanyId",
                table: "CompanySmsPatterns",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUris_CompanyId",
                table: "CompanyUris",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypes_CompanyTypeId",
                table: "ContentTypes",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypes_ContentTypeName",
                table: "ContentTypes",
                column: "ContentTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_ContentTypes_OrderContentType",
                table: "ContentTypes",
                column: "OrderContentType");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_PermissionId",
                table: "GroupPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTypes_CompanyTypeId",
                table: "PackageTypes",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTypes_OrderPackageType",
                table: "PackageTypes",
                column: "OrderPackageType");

            migrationBuilder.CreateIndex(
                name: "IX_PackageTypes_PackageTypeName",
                table: "PackageTypes",
                column: "PackageTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleGroups_GroupId",
                table: "RoleGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TokenBlacklists_UserId",
                table: "TokenBlacklists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanies_CompanyId",
                table: "UserCompanies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_Active",
                table: "UserCompanies",
                columns: new[] { "UserId", "CompanyId" },
                unique: true,
                filter: "[Deleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_GroupId",
                table: "UserGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissionVersionControls_UserId",
                table: "UserPermissionVersionControls",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_Active",
                table: "UserProfiles",
                column: "UserId",
                unique: true,
                filter: "[Deleted] = 0");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CompanyCommissions");

            migrationBuilder.DropTable(
                name: "CompanyContentTypes");

            migrationBuilder.DropTable(
                name: "CompanyDomesticPathChargeContentTypes");

            migrationBuilder.DropTable(
                name: "CompanyDomesticPathStructPriceMunicipalAreas");

            migrationBuilder.DropTable(
                name: "CompanyInsurancesChargePaymentContentTypes");

            migrationBuilder.DropTable(
                name: "CompanyInsurancesChargePayments");

            migrationBuilder.DropTable(
                name: "CompanyPackageTypes");

            migrationBuilder.DropTable(
                name: "CompanyPreferences");

            migrationBuilder.DropTable(
                name: "CompanySmsPatterns");

            migrationBuilder.DropTable(
                name: "CompanyUris");

            migrationBuilder.DropTable(
                name: "GroupPermissions");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "RoleGroups");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "TokenBlacklists");

            migrationBuilder.DropTable(
                name: "UserCompanies");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "UserPermissionVersionControls");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "CompanyDomesticPathCharges");

            migrationBuilder.DropTable(
                name: "CompanyDomesticPathStructPrices");

            migrationBuilder.DropTable(
                name: "ContentTypes");

            migrationBuilder.DropTable(
                name: "CompanyInsuranceCharges");

            migrationBuilder.DropTable(
                name: "PackageTypes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CompanyDomesticPaths");

            migrationBuilder.DropTable(
                name: "CompanyInsurances");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "CompanyTypes");
        }
    }
}
