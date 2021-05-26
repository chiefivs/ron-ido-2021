using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class ApplyAttachmentsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplyAttachmentTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    ForArchive = table.Column<bool>(type: "boolean", nullable: false),
                    ForPortal = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyAttachmentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplyId = table.Column<long>(type: "bigint", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false),
                    Given = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Error = table.Column<string>(type: "text", nullable: true),
                    AttachmentTypeId = table.Column<long>(type: "bigint", nullable: true),
                    TypeId = table.Column<long>(type: "bigint", nullable: true),
                    FileInfoUid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyAttachments_ApplyAttachmentTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ApplyAttachmentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplyAttachments_FileInfos_FileInfoUid",
                        column: x => x.FileInfoUid,
                        principalTable: "FileInfos",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplyAttachments_FileInfoUid",
                table: "ApplyAttachments",
                column: "FileInfoUid");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyAttachments_TypeId",
                table: "ApplyAttachments",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyAttachments");

            migrationBuilder.DropTable(
                name: "ApplyAttachmentTypes");
        }
    }
}
