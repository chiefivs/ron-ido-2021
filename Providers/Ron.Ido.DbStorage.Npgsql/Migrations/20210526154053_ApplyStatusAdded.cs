using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class ApplyStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptDate",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangeTime",
                table: "Applies",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StatusId1",
                table: "Applies",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplyStatusHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplyId = table.Column<long>(type: "bigint", nullable: false),
                    ChangeTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    PrevStatusId = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyStatusHistory_Applies_ApplyId",
                        column: x => x.ApplyId,
                        principalTable: "Applies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplyStatusHistory_ApplyStatuses_PrevStatusId",
                        column: x => x.PrevStatusId,
                        principalTable: "ApplyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplyStatusHistory_ApplyStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ApplyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplyStatusHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applies_StatusId1",
                table: "Applies",
                column: "StatusId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistory_ApplyId",
                table: "ApplyStatusHistory",
                column: "ApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistory_ChangeTime",
                table: "ApplyStatusHistory",
                column: "ChangeTime");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistory_EndTime",
                table: "ApplyStatusHistory",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistory_PrevStatusId",
                table: "ApplyStatusHistory",
                column: "PrevStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistory_StatusId",
                table: "ApplyStatusHistory",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusHistory_UserId",
                table: "ApplyStatusHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applies_ApplyStatuses_StatusId1",
                table: "Applies",
                column: "StatusId1",
                principalTable: "ApplyStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applies_ApplyStatuses_StatusId1",
                table: "Applies");

            migrationBuilder.DropTable(
                name: "ApplyStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_Applies_StatusId1",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "AcceptDate",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "StatusChangeTime",
                table: "Applies");

            migrationBuilder.DropColumn(
                name: "StatusId1",
                table: "Applies");
        }
    }
}
