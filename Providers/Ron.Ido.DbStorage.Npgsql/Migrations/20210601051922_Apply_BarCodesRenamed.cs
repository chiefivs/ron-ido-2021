using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Apply_BarCodesRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateBarCode",
                table: "Applies",
                newName: "PrimaryBarCode");

            migrationBuilder.RenameColumn(
                name: "AcceptBarCode",
                table: "Applies",
                newName: "BarCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrimaryBarCode",
                table: "Applies",
                newName: "CreateBarCode");

            migrationBuilder.RenameColumn(
                name: "BarCode",
                table: "Applies",
                newName: "AcceptBarCode");
        }
    }
}
