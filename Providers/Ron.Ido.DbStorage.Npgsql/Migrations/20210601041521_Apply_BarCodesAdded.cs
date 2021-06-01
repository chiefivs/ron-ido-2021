using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Apply_BarCodesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyBarCodes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcceptBarCode",
                table: "Applies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateBarCode",
                table: "Applies",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptBarCode",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreateBarCode",
                table: "Applies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.CreateTable(
                name: "ApplyBarCodes",
                columns: table => new
                {
                    BarCode = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    ApplyId = table.Column<long>(type: "bigint", nullable: false),
                    AssignTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyBarCodes", x => x.BarCode);
                    table.ForeignKey(
                        name: "FK_ApplyBarCodes_Applies_ApplyId",
                        column: x => x.ApplyId,
                        principalTable: "Applies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplyBarCodes_ApplyId",
                table: "ApplyBarCodes",
                column: "ApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyBarCodes_AssignTime",
                table: "ApplyBarCodes",
                column: "AssignTime");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyBarCodes_BarCode",
                table: "ApplyBarCodes",
                column: "BarCode");
        }
    }
}
