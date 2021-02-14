using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.SqlServer.Migrations
{
    public partial class FileInfoOldIdIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FileInfos_OldId",
                table: "FileInfos",
                column: "OldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileInfos_OldId",
                table: "FileInfos");
        }
    }
}
