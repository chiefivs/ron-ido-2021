using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class EmailsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyAttachments_FileInfos_FileInfoUid",
                table: "ApplyAttachments");

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    From = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    To = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    MessageId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    InReplyTo = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: true),
                    Subject = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailAttachments",
                columns: table => new
                {
                    EmailId = table.Column<long>(type: "bigint", nullable: false),
                    FileInfoUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAttachments", x => new { x.EmailId, x.FileInfoUid });
                    table.ForeignKey(
                        name: "FK_EmailAttachments_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailAttachments_FileInfos_FileInfoUid",
                        column: x => x.FileInfoUid,
                        principalTable: "FileInfos",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailAttachments_FileInfoUid",
                table: "EmailAttachments",
                column: "FileInfoUid");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_InReplyTo",
                table: "Emails",
                column: "InReplyTo");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_MessageId",
                table: "Emails",
                column: "MessageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyAttachments_FileInfos_FileInfoUid",
                table: "ApplyAttachments",
                column: "FileInfoUid",
                principalTable: "FileInfos",
                principalColumn: "Uid",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyAttachments_FileInfos_FileInfoUid",
                table: "ApplyAttachments");

            migrationBuilder.DropTable(
                name: "EmailAttachments");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyAttachments_FileInfos_FileInfoUid",
                table: "ApplyAttachments",
                column: "FileInfoUid",
                principalTable: "FileInfos",
                principalColumn: "Uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
