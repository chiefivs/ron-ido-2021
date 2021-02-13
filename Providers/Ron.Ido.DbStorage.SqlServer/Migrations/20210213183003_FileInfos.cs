using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.SqlServer.Migrations
{
    public partial class FileInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileInfos",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    OldId = table.Column<int>(type: "int", nullable: false),
                    CreatorEmail = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfos", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_FileInfos_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileInfos_CreatedById",
                table: "FileInfos",
                column: "CreatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileInfos");
        }
    }
}
