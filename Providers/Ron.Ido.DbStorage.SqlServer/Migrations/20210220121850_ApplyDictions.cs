using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.SqlServer.Migrations
{
    public partial class ApplyDictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplyAims",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderNum = table.Column<int>(type: "int", nullable: false),
                    NameEng = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyAims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyDocFullPackageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyDocFullPackageTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplyAims_Name",
                table: "ApplyAims",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyAims_NameEng",
                table: "ApplyAims",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocFullPackageTypes_Name",
                table: "ApplyDocFullPackageTypes",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyAims");

            migrationBuilder.DropTable(
                name: "ApplyDocFullPackageTypes");
        }
    }
}
