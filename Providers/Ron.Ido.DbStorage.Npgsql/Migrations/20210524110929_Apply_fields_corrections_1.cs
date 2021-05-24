using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Apply_fields_corrections_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyPassportTypes_CreatorPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyPassportTypes_OwnerPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_CreatorPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_OwnerPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreatorPassportTypeId1",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "OwnerPassportTypeId1",
                table: "Applies");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerPassportTypeId",
                table: "Applies",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "DocCountryId",
                table: "Applies",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorPassportTypeId",
                table: "Applies",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applies_CreatorPassportTypeId",
                table: "Applies",
                column: "CreatorPassportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_EpguCode",
                table: "Applies",
                column: "EpguCode");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_OwnerPassportTypeId",
                table: "Applies",
                column: "OwnerPassportTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyPassportTypes_CreatorPassportTypeId",
                table: "Applies",
                column: "CreatorPassportTypeId",
                principalTable: "ApplyPassportTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyPassportTypes_OwnerPassportTypeId",
                table: "Applies",
                column: "OwnerPassportTypeId",
                principalTable: "ApplyPassportTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyPassportTypes_CreatorPassportTypeId",
                table: "Applies");

            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyPassportTypes_OwnerPassportTypeId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_CreatorPassportTypeId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_EpguCode",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_OwnerPassportTypeId",
                table: "Applies");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerPassportTypeId",
                table: "Applies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocCountryId",
                table: "Applies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorPassportTypeId",
                table: "Applies",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorPassportTypeId1",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerPassportTypeId1",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applies_CreatorPassportTypeId1",
                table: "Applies",
                column: "CreatorPassportTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_OwnerPassportTypeId1",
                table: "Applies",
                column: "OwnerPassportTypeId1");

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
    }
}
