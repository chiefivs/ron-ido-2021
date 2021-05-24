using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Correct_indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LearnLevels_BeginDate",
                table: "LearnLevels",
                column: "BeginDate");

            migrationBuilder.CreateIndex(
                name: "IX_LearnLevels_EndDate",
                table: "LearnLevels",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_LearnLevels_OrderNum",
                table: "LearnLevels",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyTemplates_OrderNum",
                table: "ApplyTemplates",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyPassportTypes_Code",
                table: "ApplyPassportTypes",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyPassportTypes_OrderNum",
                table: "ApplyPassportTypes",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyLearnForms_OrderNum",
                table: "ApplyLearnForms",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyEntryForms_OrderNum",
                table: "ApplyEntryForms",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_BeginDate",
                table: "ApplyDocTypes",
                column: "BeginDate");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_EndDate",
                table: "ApplyDocTypes",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_OrderNum",
                table: "ApplyDocTypes",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocFullPackageTypes_OrderNum",
                table: "ApplyDocFullPackageTypes",
                column: "OrderNum");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDeliveryForms_OrderNum",
                table: "ApplyDeliveryForms",
                column: "OrderNum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LearnLevels_BeginDate",
                table: "LearnLevels");

            migrationBuilder.DropIndex(
                name: "IX_LearnLevels_EndDate",
                table: "LearnLevels");

            migrationBuilder.DropIndex(
                name: "IX_LearnLevels_OrderNum",
                table: "LearnLevels");

            migrationBuilder.DropIndex(
                name: "IX_ApplyTemplates_OrderNum",
                table: "ApplyTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ApplyPassportTypes_Code",
                table: "ApplyPassportTypes");

            migrationBuilder.DropIndex(
                name: "IX_ApplyPassportTypes_OrderNum",
                table: "ApplyPassportTypes");

            migrationBuilder.DropIndex(
                name: "IX_ApplyLearnForms_OrderNum",
                table: "ApplyLearnForms");

            migrationBuilder.DropIndex(
                name: "IX_ApplyEntryForms_OrderNum",
                table: "ApplyEntryForms");

            migrationBuilder.DropIndex(
                name: "IX_ApplyDocTypes_BeginDate",
                table: "ApplyDocTypes");

            migrationBuilder.DropIndex(
                name: "IX_ApplyDocTypes_EndDate",
                table: "ApplyDocTypes");

            migrationBuilder.DropIndex(
                name: "IX_ApplyDocTypes_OrderNum",
                table: "ApplyDocTypes");

            migrationBuilder.DropIndex(
                name: "IX_ApplyDocFullPackageTypes_OrderNum",
                table: "ApplyDocFullPackageTypes");

            migrationBuilder.DropIndex(
                name: "IX_ApplyDeliveryForms_OrderNum",
                table: "ApplyDeliveryForms");
        }
    }
}
