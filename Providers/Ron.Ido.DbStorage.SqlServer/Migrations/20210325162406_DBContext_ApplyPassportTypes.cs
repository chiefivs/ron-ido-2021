using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.SqlServer.Migrations
{
    public partial class DBContext_ApplyPassportTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplyPassportTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyPassportTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplyPassportTypes_Name",
                table: "ApplyPassportTypes",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyPassportTypes");
        }
    }
}
