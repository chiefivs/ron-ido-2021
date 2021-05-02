using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyAims",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyAims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyDeliveryForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyDeliveryForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyDocFullPackageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyDocFullPackageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyEntryForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyEntryForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyLearnForms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyLearnForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyPassportTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyPassportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    OrderNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearnLevels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Snils = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplyBarCodes",
                columns: table => new
                {
                    BarCode = table.Column<string>(type: "text", nullable: false),
                    ApplyId = table.Column<long>(type: "bigint", nullable: false),
                    AssignTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyBarCodes", x => x.BarCode);
                    table.ForeignKey(
                        name: "FK_ApplyBarCodes_Applies_ApplyId",
                        column: x => x.ApplyId,
                        principalTable: "Applies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplyDocTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    OrderNum = table.Column<int>(type: "integer", nullable: false),
                    LearnLevelId = table.Column<long>(type: "bigint", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NameEng = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyDocTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplyDocTypes_LearnLevels_LearnLevelId",
                        column: x => x.LearnLevelId,
                        principalTable: "LearnLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileInfos",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(260)", maxLength: 260, nullable: true),
                    Size = table.Column<int>(type: "integer", nullable: false),
                    ContentType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    OldId = table.Column<int>(type: "integer", nullable: false),
                    CreatorEmail = table.Column<string>(type: "character varying(260)", maxLength: 260, nullable: true),
                    Source = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_ApplyBarCodes_ApplyId",
                table: "ApplyBarCodes",
                column: "ApplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyBarCodes_AssignTime",
                table: "ApplyBarCodes",
                column: "AssignTime");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDeliveryForms_Name",
                table: "ApplyDeliveryForms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDeliveryForms_NameEng",
                table: "ApplyDeliveryForms",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocFullPackageTypes_Name",
                table: "ApplyDocFullPackageTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_LearnLevelId",
                table: "ApplyDocTypes",
                column: "LearnLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_Name",
                table: "ApplyDocTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyDocTypes_NameEng",
                table: "ApplyDocTypes",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyEntryForms_Name",
                table: "ApplyEntryForms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyLearnForms_Name",
                table: "ApplyLearnForms",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyLearnForms_NameEng",
                table: "ApplyLearnForms",
                column: "NameEng");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyPassportTypes_Name",
                table: "ApplyPassportTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyTemplates_Name",
                table: "ApplyTemplates",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_FileInfos_CreatedById",
                table: "FileInfos",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FileInfos_OldId",
                table: "FileInfos",
                column: "OldId");

            migrationBuilder.CreateIndex(
                name: "IX_LearnLevels_FullName",
                table: "LearnLevels",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_LearnLevels_Name",
                table: "LearnLevels",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_RoleId",
                table: "UsersRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyAims");

            migrationBuilder.DropTable(
                name: "ApplyBarCodes");

            migrationBuilder.DropTable(
                name: "ApplyDeliveryForms");

            migrationBuilder.DropTable(
                name: "ApplyDocFullPackageTypes");

            migrationBuilder.DropTable(
                name: "ApplyDocTypes");

            migrationBuilder.DropTable(
                name: "ApplyEntryForms");

            migrationBuilder.DropTable(
                name: "ApplyLearnForms");

            migrationBuilder.DropTable(
                name: "ApplyPassportTypes");

            migrationBuilder.DropTable(
                name: "ApplyTemplates");

            migrationBuilder.DropTable(
                name: "FileInfos");

            migrationBuilder.DropTable(
                name: "RolesPermissions");

            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "Applies");

            migrationBuilder.DropTable(
                name: "LearnLevels");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
