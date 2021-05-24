using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class ApplyStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AimId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BaseLearnDateBegin",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BaseLearnDateEnd",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorCitizenshipId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorCountryId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Applies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeliveryFormId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocAttachmentsCount",
                table: "Applies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocBlankNum",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocDate",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocDateYear",
                table: "Applies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocFullName",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocRegNum",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DocTypeId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DocsWillSendByPost",
                table: "Applies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "EntryFormId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EpguStatus",
                table: "Applies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FixedLearnSpecialityName",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ForInfoLetter",
                table: "Applies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ForOferta",
                table: "Applies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnglish",
                table: "Applies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNovorossia",
                table: "Applies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRostovFilial",
                table: "Applies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OrgCreator",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerCitizenshipId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerCountryId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ReturnOriginalsFormId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolAddress",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolCityName",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SchoolCountryId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolEmail",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolFax",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolName",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolPhone",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolPostIndex",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SchoolTypeId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SpecialLearnDateBegin",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SpecialLearnDateEnd",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SpecialLearnFormId",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Applies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Storage",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TransmitOpenChannels",
                table: "Applies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CertificateDeliveryForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificateDeliveryForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReglamentEtap",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MinTerm = table.Column<int>(type: "integer", nullable: false),
                    MaxTerm = table.Column<int>(type: "integer", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReglamentEtap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NameEng = table.Column<string>(type: "text", nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyCertificateDeliveryForms",
                columns: table => new
                {
                    ApplyId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryFormId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryFormId1 = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyCertificateDeliveryForms", x => new { x.ApplyId, x.DeliveryFormId });
                    table.ForeignKey(
                        name: "FK_ApplyCertificateDeliveryForms_Applies_DeliveryFormId",
                        column: x => x.DeliveryFormId,
                        principalTable: "Applies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplyCertificateDeliveryForms_CertificateDeliveryForms_Deli~",
                        column: x => x.DeliveryFormId1,
                        principalTable: "CertificateDeliveryForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplyStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StatusEnumValue = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NameForButton = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NameForApplier = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NameForApplierEng = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DescriptionForApplier = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DescriptionForApplierEng = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    VisibleForApplier = table.Column<bool>(type: "boolean", nullable: false),
                    AllowStepToStatuses = table.Column<string>(type: "text", nullable: true),
                    EtapId = table.Column<int>(type: "integer", nullable: true),
                    EtapId1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyStatuses_ReglamentEtap_EtapId1",
                        column: x => x.EtapId1,
                        principalTable: "ReglamentEtap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplyStatusesPermissions",
                columns: table => new
                {
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    IsViewAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    IsStepAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    ApplyStatusId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyStatusesPermissions", x => new { x.StatusId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ApplyStatusesPermissions_ApplyStatuses_ApplyStatusId",
                        column: x => x.ApplyStatusId,
                        principalTable: "ApplyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applies_AimId",
                table: "Applies",
                column: "AimId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_CreatorCitizenshipId",
                table: "Applies",
                column: "CreatorCitizenshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_CreatorCountryId",
                table: "Applies",
                column: "CreatorCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_DeliveryFormId",
                table: "Applies",
                column: "DeliveryFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_DocCountryId",
                table: "Applies",
                column: "DocCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_DocTypeId",
                table: "Applies",
                column: "DocTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_EntryFormId",
                table: "Applies",
                column: "EntryFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_OwnerCitizenshipId",
                table: "Applies",
                column: "OwnerCitizenshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_OwnerCountryId",
                table: "Applies",
                column: "OwnerCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_ReturnOriginalsFormId",
                table: "Applies",
                column: "ReturnOriginalsFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_SchoolCountryId",
                table: "Applies",
                column: "SchoolCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_SchoolTypeId",
                table: "Applies",
                column: "SchoolTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_SpecialLearnFormId",
                table: "Applies",
                column: "SpecialLearnFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyCertificateDeliveryForms_DeliveryFormId",
                table: "ApplyCertificateDeliveryForms",
                column: "DeliveryFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyCertificateDeliveryForms_DeliveryFormId1",
                table: "ApplyCertificateDeliveryForms",
                column: "DeliveryFormId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatuses_EtapId1",
                table: "ApplyStatuses",
                column: "EtapId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusesPermissions_ApplyStatusId",
                table: "ApplyStatusesPermissions",
                column: "ApplyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusesPermissions_IsStepAllowed",
                table: "ApplyStatusesPermissions",
                column: "IsStepAllowed");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusesPermissions_IsViewAllowed",
                table: "ApplyStatusesPermissions",
                column: "IsViewAllowed");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateDeliveryForms_Name",
                table: "CertificateDeliveryForms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateDeliveryForms_NameEng",
                table: "CertificateDeliveryForms",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_CertificateDeliveryForms_OrderNum",
                table: "CertificateDeliveryForms",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ReglamentEtap_Name",
                table: "ReglamentEtap",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ReglamentEtap_OrderNum",
                table: "ReglamentEtap",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_Name",
                table: "SchoolType",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_NameEng",
                table: "SchoolType",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolType_OrderNum",
                table: "SchoolType",
                column: "OrderNum");

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyAims_AimId",
                table: "Applies",
                column: "AimId",
                principalTable: "ApplyAims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyDeliveryForms_DeliveryFormId",
                table: "Applies",
                column: "DeliveryFormId",
                principalTable: "ApplyDeliveryForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyDeliveryForms_ReturnOriginalsFormId",
                table: "Applies",
                column: "ReturnOriginalsFormId",
                principalTable: "ApplyDeliveryForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyDocTypes_DocTypeId",
                table: "Applies",
                column: "DocTypeId",
                principalTable: "ApplyDocTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyEntryForms_EntryFormId",
                table: "Applies",
                column: "EntryFormId",
                principalTable: "ApplyEntryForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyLearnForms_SpecialLearnFormId",
                table: "Applies",
                column: "SpecialLearnFormId",
                principalTable: "ApplyLearnForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_Countries_CreatorCitizenshipId",
                table: "Applies",
                column: "CreatorCitizenshipId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_Countries_CreatorCountryId",
                table: "Applies",
                column: "CreatorCountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_Countries_DocCountryId",
                table: "Applies",
                column: "DocCountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_Countries_OwnerCitizenshipId",
                table: "Applies",
                column: "OwnerCitizenshipId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_Countries_OwnerCountryId",
                table: "Applies",
                column: "OwnerCountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_Countries_SchoolCountryId",
                table: "Applies",
                column: "SchoolCountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_SchoolType_SchoolTypeId",
                table: "Applies",
                column: "SchoolTypeId",
                principalTable: "SchoolType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyAims_AimId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyDeliveryForms_DeliveryFormId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyDeliveryForms_ReturnOriginalsFormId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyDocTypes_DocTypeId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyEntryForms_EntryFormId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyLearnForms_SpecialLearnFormId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_Countries_CreatorCitizenshipId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_Countries_CreatorCountryId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_Countries_DocCountryId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_Countries_OwnerCitizenshipId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_Countries_OwnerCountryId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_Countries_SchoolCountryId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_SchoolType_SchoolTypeId",
                table: "Applies");

            migrationBuilder.DropTable(
                name: "ApplyCertificateDeliveryForms");

            migrationBuilder.DropTable(
                name: "ApplyStatusesPermissions");

            migrationBuilder.DropTable(
                name: "SchoolType");

            migrationBuilder.DropTable(
                name: "CertificateDeliveryForms");

            migrationBuilder.DropTable(
                name: "ApplyStatuses");

            migrationBuilder.DropTable(
                name: "ReglamentEtap");

            migrationBuilder.DropIndex(
                name: "IX_Applies_AimId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_CreatorCitizenshipId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_CreatorCountryId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_DeliveryFormId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_DocCountryId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_DocTypeId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_EntryFormId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_OwnerCitizenshipId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_OwnerCountryId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_ReturnOriginalsFormId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_SchoolCountryId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_SchoolTypeId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_SpecialLearnFormId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "AimId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "BaseLearnDateBegin",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "BaseLearnDateEnd",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorCitizenshipId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorCountryId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "DeliveryFormId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "DocAttachmentsCount",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "DocBlankNum",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "DocDate",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "DocDateYear",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "DocFullName",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "DocRegNum",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "DocTypeId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "DocsWillSendByPost",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "EntryFormId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "EpguStatus",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "FixedLearnSpecialityName",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "ForInfoLetter",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "ForOferta",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "IsEnglish",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "IsNovorossia",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "IsRostovFilial",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OrgCreator",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "Other",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerCitizenshipId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerCountryId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "ReturnOriginalsFormId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SchoolAddress",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SchoolCityName",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SchoolCountryId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SchoolEmail",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SchoolFax",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SchoolName",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SchoolPhone",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SchoolPostIndex",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SchoolTypeId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SpecialLearnDateBegin",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SpecialLearnDateEnd",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "SpecialLearnFormId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "Storage",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "TransmitOpenChannels",
                table: "Applies");
        }
    }
}
