using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Apply_CreateDateAndAcceptDate_renamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Applies",
                newName: "CreateTime");

            migrationBuilder.RenameColumn(
                name: "AcceptDate",
                table: "Applies",
                newName: "AcceptTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "Applies",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "AcceptTime",
                table: "Applies",
                newName: "AcceptDate");
        }
    }
}
