using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Apply_Attachments_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ApplyAttachments_ApplyId",
                table: "ApplyAttachments",
                column: "ApplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyAttachments_Applies_ApplyId",
                table: "ApplyAttachments",
                column: "ApplyId",
                principalTable: "Applies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyAttachments_Applies_ApplyId",
                table: "ApplyAttachments");

            migrationBuilder.DropIndex(
                name: "IX_ApplyAttachments_ApplyId",
                table: "ApplyAttachments");
        }
    }
}
