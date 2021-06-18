using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class DossierCommentsAndApplyMessagesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplyMessages",
                columns: table => new
                {
                    ApplyId = table.Column<long>(type: "bigint", nullable: false),
                    FieldName = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyMessages", x => new { x.ApplyId, x.FieldName });
                });

            migrationBuilder.CreateTable(
                name: "DossierComments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DossierId = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DossierComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DossierComments_Dossiers_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DossierComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DossierCommentAttachments",
                columns: table => new
                {
                    CommentId = table.Column<long>(type: "bigint", nullable: false),
                    FileInfoUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DossierCommentAttachments", x => new { x.CommentId, x.FileInfoUid });
                    table.ForeignKey(
                        name: "FK_DossierCommentAttachments_DossierComments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "DossierComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DossierCommentAttachments_FileInfos_FileInfoUid",
                        column: x => x.FileInfoUid,
                        principalTable: "FileInfos",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DossierCommentAttachments_FileInfoUid",
                table: "DossierCommentAttachments",
                column: "FileInfoUid");

            migrationBuilder.CreateIndex(
                name: "IX_DossierComments_DossierId",
                table: "DossierComments",
                column: "DossierId");

            migrationBuilder.CreateIndex(
                name: "IX_DossierComments_UserId",
                table: "DossierComments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyMessages");

            migrationBuilder.DropTable(
                name: "DossierCommentAttachments");

            migrationBuilder.DropTable(
                name: "DossierComments");
        }
    }
}
