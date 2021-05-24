using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Countries_and_Regions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BarCode",
                table: "ApplyBarCodes",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "ByWarrant",
                table: "Applies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatorBirthDate",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorBlock",
                table: "Applies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorBuilding",
                table: "Applies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorCityName",
                table: "Applies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorCorpus",
                table: "Applies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmail",
                table: "Applies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorFirstName",
                table: "Applies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorFlat",
                table: "Applies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorLastName",
                table: "Applies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorMailIndex",
                table: "Applies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorPassportReq",
                table: "Applies",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorPassportTypeId",
                table: "Applies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorPassportTypeId1",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorPhone",
                table: "Applies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorStreet",
                table: "Applies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorSurname",
                table: "Applies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocCountryId",
                table: "Applies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EpguCode",
                table: "Applies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnerBirthDate",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerBlock",
                table: "Applies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerBuilding",
                table: "Applies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerCityName",
                table: "Applies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerCorpus",
                table: "Applies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerFirstName",
                table: "Applies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerFlat",
                table: "Applies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerLastName",
                table: "Applies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerMailIndex",
                table: "Applies",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerPassportReq",
                table: "Applies",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerPassportTypeId",
                table: "Applies",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerPassportTypeId1",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerPhone",
                table: "Applies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerStreet",
                table: "Applies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerSurname",
                table: "Applies",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantDate",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WarrantReq",
                table: "Applies",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantTerm",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Legalizations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legalizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FullName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    NameEng = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LegalizationNeeded = table.Column<bool>(type: "boolean", nullable: true),
                    LegalizationComment = table.Column<string>(type: "text", nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    A2code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    A3code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    EiisCode = table.Column<int>(type: "integer", nullable: true),
                    IsgaCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OksmCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RegionId = table.Column<int>(type: "integer", nullable: true),
                    RegionId1 = table.Column<long>(type: "bigint", nullable: true),
                    LegalizationId = table.Column<int>(type: "integer", nullable: true),
                    CoordX = table.Column<double>(type: "double precision", nullable: true),
                    CoordY = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Regions_RegionId1",
                        column: x => x.RegionId1,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplyBarCodes_BarCode",
                table: "ApplyBarCodes",
                column: "BarCode");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_CreatorPassportTypeId1",
                table: "Applies",
                column: "CreatorPassportTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_OwnerPassportTypeId1",
                table: "Applies",
                column: "OwnerPassportTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_A2code",
                table: "Countries",
                column: "A2code");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_A3code",
                table: "Countries",
                column: "A3code");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_EiisCode",
                table: "Countries",
                column: "EiisCode");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_IsgaCode",
                table: "Countries",
                column: "IsgaCode");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_NameEng",
                table: "Countries",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_OksmCode",
                table: "Countries",
                column: "OksmCode");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_OrderNum",
                table: "Countries",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RegionId1",
                table: "Countries",
                column: "RegionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Legalizations_Name",
                table: "Legalizations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Legalizations_OrderNum",
                table: "Legalizations",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_Name",
                table: "Regions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_OrderNum",
                table: "Regions",
                column: "OrderNum");

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyPassportTypes_CreatorPassportTypeId1",
                table: "Applies",
                column: "CreatorPassportTypeId1",
                principalTable: "ApplyPassportTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyPassportTypes_OwnerPassportTypeId1",
                table: "Applies",
                column: "OwnerPassportTypeId1",
                principalTable: "ApplyPassportTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyPassportTypes_CreatorPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyPassportTypes_OwnerPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Legalizations");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_ApplyBarCodes_BarCode",
                table: "ApplyBarCodes");

            migrationBuilder.DropIndex(
                name: "IX_Applies_CreatorPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_OwnerPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "ByWarrant",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorBirthDate",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorBlock",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorBuilding",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorCityName",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorCorpus",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorEmail",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorFirstName",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorFlat",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorLastName",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorMailIndex",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorPassportReq",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorPassportTypeId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorPhone",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorStreet",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorSurname",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "DocCountryId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "EpguCode",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerBirthDate",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerBlock",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerBuilding",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerCityName",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerCorpus",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerFirstName",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerFlat",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerLastName",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerMailIndex",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerPassportReq",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerPassportTypeId",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerPhone",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerStreet",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerSurname",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "WarrantDate",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "WarrantReq",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "WarrantTerm",
                table: "Applies");

            migrationBuilder.AlterColumn<string>(
                name: "BarCode",
                table: "ApplyBarCodes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);
        }
    }
}
