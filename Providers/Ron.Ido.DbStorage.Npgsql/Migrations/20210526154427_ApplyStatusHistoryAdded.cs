using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class ApplyStatusHistoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatusHistory_Applies_ApplyId",
                table: "ApplyStatusHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatusHistory_ApplyStatuses_PrevStatusId",
                table: "ApplyStatusHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatusHistory_ApplyStatuses_StatusId",
                table: "ApplyStatusHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatusHistory_Users_UserId",
                table: "ApplyStatusHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplyStatusHistory",
                table: "ApplyStatusHistory");

            migrationBuilder.RenameTable(
                name: "ApplyStatusHistory",
                newName: "ApplyStatusHistories");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistory_UserId",
                table: "ApplyStatusHistories",
                newName: "IX_ApplyStatusHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistory_StatusId",
                table: "ApplyStatusHistories",
                newName: "IX_ApplyStatusHistories_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistory_PrevStatusId",
                table: "ApplyStatusHistories",
                newName: "IX_ApplyStatusHistories_PrevStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistory_EndTime",
                table: "ApplyStatusHistories",
                newName: "IX_ApplyStatusHistories_EndTime");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistory_ChangeTime",
                table: "ApplyStatusHistories",
                newName: "IX_ApplyStatusHistories_ChangeTime");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistory_ApplyId",
                table: "ApplyStatusHistories",
                newName: "IX_ApplyStatusHistories_ApplyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplyStatusHistories",
                table: "ApplyStatusHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatusHistories_Applies_ApplyId",
                table: "ApplyStatusHistories",
                column: "ApplyId",
                principalTable: "Applies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatusHistories_ApplyStatuses_PrevStatusId",
                table: "ApplyStatusHistories",
                column: "PrevStatusId",
                principalTable: "ApplyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatusHistories_ApplyStatuses_StatusId",
                table: "ApplyStatusHistories",
                column: "StatusId",
                principalTable: "ApplyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatusHistories_Users_UserId",
                table: "ApplyStatusHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatusHistories_Applies_ApplyId",
                table: "ApplyStatusHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatusHistories_ApplyStatuses_PrevStatusId",
                table: "ApplyStatusHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatusHistories_ApplyStatuses_StatusId",
                table: "ApplyStatusHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatusHistories_Users_UserId",
                table: "ApplyStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplyStatusHistories",
                table: "ApplyStatusHistories");

            migrationBuilder.RenameTable(
                name: "ApplyStatusHistories",
                newName: "ApplyStatusHistory");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistories_UserId",
                table: "ApplyStatusHistory",
                newName: "IX_ApplyStatusHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistories_StatusId",
                table: "ApplyStatusHistory",
                newName: "IX_ApplyStatusHistory_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistories_PrevStatusId",
                table: "ApplyStatusHistory",
                newName: "IX_ApplyStatusHistory_PrevStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistories_EndTime",
                table: "ApplyStatusHistory",
                newName: "IX_ApplyStatusHistory_EndTime");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistories_ChangeTime",
                table: "ApplyStatusHistory",
                newName: "IX_ApplyStatusHistory_ChangeTime");

            migrationBuilder.RenameIndex(
                name: "IX_ApplyStatusHistories_ApplyId",
                table: "ApplyStatusHistory",
                newName: "IX_ApplyStatusHistory_ApplyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplyStatusHistory",
                table: "ApplyStatusHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatusHistory_Applies_ApplyId",
                table: "ApplyStatusHistory",
                column: "ApplyId",
                principalTable: "Applies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatusHistory_ApplyStatuses_PrevStatusId",
                table: "ApplyStatusHistory",
                column: "PrevStatusId",
                principalTable: "ApplyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatusHistory_ApplyStatuses_StatusId",
                table: "ApplyStatusHistory",
                column: "StatusId",
                principalTable: "ApplyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatusHistory_Users_UserId",
                table: "ApplyStatusHistory",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
