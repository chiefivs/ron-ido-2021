using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class ApplyStatusHistory_UserId_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatusHistories_Users_UserId",
                table: "ApplyStatusHistories");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "ApplyStatusHistories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatusHistories_Users_UserId",
                table: "ApplyStatusHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatusHistories_Users_UserId",
                table: "ApplyStatusHistories");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "ApplyStatusHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatusHistories_Users_UserId",
                table: "ApplyStatusHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
