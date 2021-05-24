using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Correct_keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Regions_RegionId1",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_RegionId1",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "RegionId1",
                table: "Countries");

            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "Regions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Legalizations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "RegionId",
                table: "Countries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "Countries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_OldId",
                table: "Regions",
                column: "OldId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_OldId",
                table: "Countries",
                column: "OldId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RegionId",
                table: "Countries",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Regions_RegionId",
                table: "Countries",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Regions_RegionId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Regions_OldId",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Countries_OldId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_RegionId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "Countries");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Legalizations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "Countries",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RegionId1",
                table: "Countries",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RegionId1",
                table: "Countries",
                column: "RegionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Regions_RegionId1",
                table: "Countries",
                column: "RegionId1",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
