using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capitan360.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addNewChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Area_CityId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Area_CountryId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Area_ProvinceId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Area_Area_ParentId",
                table: "Area");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAddress_Address_AddressId",
                table: "CompanyAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAddress_Companies_CompanyId",
                table: "CompanyAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyAddress",
                table: "CompanyAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Area",
                table: "Area");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "CompanyAddress",
                newName: "CompanyAddresses");

            migrationBuilder.RenameTable(
                name: "Area",
                newName: "Areas");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyAddress_CompanyId",
                table: "CompanyAddresses",
                newName: "IX_CompanyAddresses_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyAddress_AddressId",
                table: "CompanyAddresses",
                newName: "IX_CompanyAddresses_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Area_ParentId",
                table: "Areas",
                newName: "IX_Areas_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ProvinceId",
                table: "Addresses",
                newName: "IX_Addresses_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_CountryId",
                table: "Addresses",
                newName: "IX_Addresses_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_CityId",
                table: "Addresses",
                newName: "IX_Addresses_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyAddresses",
                table: "CompanyAddresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

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
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                name: "CompanyPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Admin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
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
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCommissions_CompanyId",
                table: "CompanyCommissions",
                column: "CompanyId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Areas_CityId",
                table: "Addresses",
                column: "CityId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Areas_CountryId",
                table: "Addresses",
                column: "CountryId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Areas_ProvinceId",
                table: "Addresses",
                column: "ProvinceId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Areas_ParentId",
                table: "Areas",
                column: "ParentId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAddresses_Addresses_AddressId",
                table: "CompanyAddresses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAddresses_Companies_CompanyId",
                table: "CompanyAddresses",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Areas_CityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Areas_CountryId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Areas_ProvinceId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Areas_ParentId",
                table: "Areas");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAddresses_Addresses_AddressId",
                table: "CompanyAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAddresses_Companies_CompanyId",
                table: "CompanyAddresses");

            migrationBuilder.DropTable(
                name: "CompanyCommissions");

            migrationBuilder.DropTable(
                name: "CompanyPreferences");

            migrationBuilder.DropTable(
                name: "CompanySmsPatterns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyAddresses",
                table: "CompanyAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "CompanyAddresses",
                newName: "CompanyAddress");

            migrationBuilder.RenameTable(
                name: "Areas",
                newName: "Area");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyAddresses_CompanyId",
                table: "CompanyAddress",
                newName: "IX_CompanyAddress_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyAddresses_AddressId",
                table: "CompanyAddress",
                newName: "IX_CompanyAddress_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_ParentId",
                table: "Area",
                newName: "IX_Area_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ProvinceId",
                table: "Address",
                newName: "IX_Address_ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CountryId",
                table: "Address",
                newName: "IX_Address_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CityId",
                table: "Address",
                newName: "IX_Address_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyAddress",
                table: "CompanyAddress",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Area",
                table: "Area",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Area_CityId",
                table: "Address",
                column: "CityId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Area_CountryId",
                table: "Address",
                column: "CountryId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Area_ProvinceId",
                table: "Address",
                column: "ProvinceId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Area_Area_ParentId",
                table: "Area",
                column: "ParentId",
                principalTable: "Area",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAddress_Address_AddressId",
                table: "CompanyAddress",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAddress_Companies_CompanyId",
                table: "CompanyAddress",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
