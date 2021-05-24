using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class ApplyStatuses2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatuses_ReglamentEtap_EtapId1",
                table: "ApplyStatuses");

            migrationBuilder.DropTable(
                name: "ApplyStatusesPermissions");

            migrationBuilder.DropIndex(
                name: "IX_ApplyStatuses_EtapId1",
                table: "ApplyStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReglamentEtap",
                table: "ReglamentEtap");

            migrationBuilder.DropColumn(
                name: "EtapId1",
                table: "ApplyStatuses");

            migrationBuilder.RenameTable(
                name: "ReglamentEtap",
                newName: "ReglamentEtaps");

            migrationBuilder.RenameIndex(
                name: "IX_ReglamentEtap_OrderNum",
                table: "ReglamentEtaps",
                newName: "IX_ReglamentEtaps_OrderNum");

            migrationBuilder.RenameIndex(
                name: "IX_ReglamentEtap_Name",
                table: "ReglamentEtaps",
                newName: "IX_ReglamentEtaps_Name");

            migrationBuilder.AddColumn<string>(
                name: "StepApplyStatusesString",
                table: "Roles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ViewApplyStatusesString",
                table: "Roles",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EtapId",
                table: "ApplyStatuses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "ApplyStatuses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReglamentEtaps",
                table: "ReglamentEtaps",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatuses_EtapId",
                table: "ApplyStatuses",
                column: "EtapId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatuses_ReglamentEtaps_EtapId",
                table: "ApplyStatuses",
                column: "EtapId",
                principalTable: "ReglamentEtaps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyStatuses_ReglamentEtaps_EtapId",
                table: "ApplyStatuses");

            migrationBuilder.DropIndex(
                name: "IX_ApplyStatuses_EtapId",
                table: "ApplyStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReglamentEtaps",
                table: "ReglamentEtaps");

            migrationBuilder.DropColumn(
                name: "StepApplyStatusesString",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ViewApplyStatusesString",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "OldId",
                table: "ApplyStatuses");

            migrationBuilder.RenameTable(
                name: "ReglamentEtaps",
                newName: "ReglamentEtap");

            migrationBuilder.RenameIndex(
                name: "IX_ReglamentEtaps_OrderNum",
                table: "ReglamentEtap",
                newName: "IX_ReglamentEtap_OrderNum");

            migrationBuilder.RenameIndex(
                name: "IX_ReglamentEtaps_Name",
                table: "ReglamentEtap",
                newName: "IX_ReglamentEtap_Name");

            migrationBuilder.AlterColumn<int>(
                name: "EtapId",
                table: "ApplyStatuses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EtapId1",
                table: "ApplyStatuses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReglamentEtap",
                table: "ReglamentEtap",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ApplyStatusesPermissions",
                columns: table => new
                {
                    StatusId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    ApplyStatusId = table.Column<long>(type: "bigint", nullable: true),
                    IsStepAllowed = table.Column<bool>(type: "boolean", nullable: false),
                    IsViewAllowed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyStatusesPermissions", x => new { x.StatusId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ApplyStatusesPermissions_ApplyStatuses_ApplyStatusId",
                        column: x => x.ApplyStatusId,
                        principalTable: "ApplyStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatuses_EtapId1",
                table: "ApplyStatuses",
                column: "EtapId1");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusesPermissions_ApplyStatusId",
                table: "ApplyStatusesPermissions",
                column: "ApplyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusesPermissions_IsStepAllowed",
                table: "ApplyStatusesPermissions",
                column: "IsStepAllowed");

            migrationBuilder.CreateIndex(
                name: "IX_ApplyStatusesPermissions_IsViewAllowed",
                table: "ApplyStatusesPermissions",
                column: "IsViewAllowed");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyStatuses_ReglamentEtap_EtapId1",
                table: "ApplyStatuses",
                column: "EtapId1",
                principalTable: "ReglamentEtap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
