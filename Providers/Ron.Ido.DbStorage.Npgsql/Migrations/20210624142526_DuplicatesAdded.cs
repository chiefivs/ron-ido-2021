using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class DuplicatesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DuplicateId",
                table: "Dossiers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DuplicateStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NameEng = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuplicateStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Duplicates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BarCode = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateUserId = table.Column<long>(type: "bigint", nullable: true),
                    HandoutTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    HandoutUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsEnglish = table.Column<bool>(type: "boolean", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    Storage = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Note = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    MailIndex = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CityName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Block = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Flat = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Corpus = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Building = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Phones = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatorCountryId = table.Column<long>(type: "bigint", nullable: true),
                    DocFullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SchoolName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DocCountryId = table.Column<long>(type: "bigint", nullable: true),
                    DocumentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    DocumentDate = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ReturnOriginalsFormId = table.Column<long>(type: "bigint", nullable: true),
                    ReturnOriginalsPostAddress = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duplicates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Duplicates_ApplyDeliveryForms_ReturnOriginalsFormId",
                        column: x => x.ReturnOriginalsFormId,
                        principalTable: "ApplyDeliveryForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Duplicates_ApplyDocTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "ApplyDocTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Duplicates_Countries_CreatorCountryId",
                        column: x => x.CreatorCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Duplicates_Countries_DocCountryId",
                        column: x => x.DocCountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Duplicates_DuplicateStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DuplicateStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Duplicates_Users_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Duplicates_Users_HandoutUserId",
                        column: x => x.HandoutUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DuplicateCertificateDeliveryForm",
                columns: table => new
                {
                    DuplicateId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryFormId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuplicateCertificateDeliveryForm", x => new { x.DuplicateId, x.DeliveryFormId });
                    table.ForeignKey(
                        name: "FK_DuplicateCertificateDeliveryForm_CertificateDeliveryForms_D~",
                        column: x => x.DeliveryFormId,
                        principalTable: "CertificateDeliveryForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DuplicateCertificateDeliveryForm_Duplicates_DuplicateId",
                        column: x => x.DuplicateId,
                        principalTable: "Duplicates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DuplicateStatusHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DuplicateId = table.Column<long>(type: "bigint", nullable: false),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    PrevStatusId = table.Column<long>(type: "bigint", nullable: true),
                    ChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuplicateStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DuplicateStatusHistories_Duplicates_DuplicateId",
                        column: x => x.DuplicateId,
                        principalTable: "Duplicates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DuplicateStatusHistories_DuplicateStatuses_PrevStatusId",
                        column: x => x.PrevStatusId,
                        principalTable: "DuplicateStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DuplicateStatusHistories_DuplicateStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DuplicateStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DuplicateStatusHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_DuplicateId",
                table: "Dossiers",
                column: "DuplicateId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyAims_OrderNum",
                table: "ApplyAims",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateCertificateDeliveryForm_DeliveryFormId",
                table: "DuplicateCertificateDeliveryForm",
                column: "DeliveryFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Duplicates_CreateUserId",
                table: "Duplicates",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Duplicates_CreatorCountryId",
                table: "Duplicates",
                column: "CreatorCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Duplicates_DocCountryId",
                table: "Duplicates",
                column: "DocCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Duplicates_DocumentTypeId",
                table: "Duplicates",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Duplicates_HandoutUserId",
                table: "Duplicates",
                column: "HandoutUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Duplicates_ReturnOriginalsFormId",
                table: "Duplicates",
                column: "ReturnOriginalsFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Duplicates_StatusId",
                table: "Duplicates",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateStatuses_Name",
                table: "DuplicateStatuses",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateStatuses_NameEng",
                table: "DuplicateStatuses",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateStatuses_OrderNum",
                table: "DuplicateStatuses",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateStatusHistories_ChangeTime",
                table: "DuplicateStatusHistories",
                column: "ChangeTime");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateStatusHistories_DuplicateId",
                table: "DuplicateStatusHistories",
                column: "DuplicateId");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateStatusHistories_EndTime",
                table: "DuplicateStatusHistories",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateStatusHistories_PrevStatusId",
                table: "DuplicateStatusHistories",
                column: "PrevStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateStatusHistories_StatusId",
                table: "DuplicateStatusHistories",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DuplicateStatusHistories_UserId",
                table: "DuplicateStatusHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dossiers_Duplicates_DuplicateId",
                table: "Dossiers",
                column: "DuplicateId",
                principalTable: "Duplicates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dossiers_Duplicates_DuplicateId",
                table: "Dossiers");

            migrationBuilder.DropTable(
                name: "DuplicateCertificateDeliveryForm");

            migrationBuilder.DropTable(
                name: "DuplicateStatusHistories");

            migrationBuilder.DropTable(
                name: "Duplicates");

            migrationBuilder.DropTable(
                name: "DuplicateStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Dossiers_DuplicateId",
                table: "Dossiers");

            migrationBuilder.DropIndex(
                name: "IX_ApplyAims_OrderNum",
                table: "ApplyAims");

            migrationBuilder.DropColumn(
                name: "DuplicateId",
                table: "Dossiers");
        }
    }
}
