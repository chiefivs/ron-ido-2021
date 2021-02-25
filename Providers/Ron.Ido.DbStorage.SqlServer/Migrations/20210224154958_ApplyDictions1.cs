using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.SqlServer.Migrations
{
    public partial class ApplyDictions1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplyLearnForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderNum = table.Column<int>(type: "int", nullable: false),
                    NameEng = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyLearnForms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplyLearnForms_Name",
                table: "ApplyLearnForms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyLearnForms_NameEng",
                table: "ApplyLearnForms",
                column: "NameEng");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyLearnForms");
        }
    }
}
