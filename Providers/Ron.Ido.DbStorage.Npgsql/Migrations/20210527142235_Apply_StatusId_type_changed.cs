using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Apply_StatusId_type_changed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyStatuses_StatusId1",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_StatusId1",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "StatusId1",
                table: "Applies");

            migrationBuilder.AlterColumn<long>(
                name: "StatusId",
                table: "Applies",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_StatusId",
                table: "Applies",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyStatuses_StatusId",
                table: "Applies",
                column: "StatusId",
                principalTable: "ApplyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyStatuses_StatusId",
                table: "Applies");

            migrationBuilder.DropIndex(
                name: "IX_Applies_StatusId",
                table: "Applies");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Applies",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "StatusId1",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applies_StatusId1",
                table: "Applies",
                column: "StatusId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyStatuses_StatusId1",
                table: "Applies",
                column: "StatusId1",
                principalTable: "ApplyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
