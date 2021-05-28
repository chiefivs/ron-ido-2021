using Microsoft.EntityFrameworkCore.Migrations;

namespace Ron.Ido.DbStorage.Npgsql.Migrations
{
    public partial class ApplyCertificateDeliveryForms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyCertificateDeliveryForms_Applies_DeliveryFormId",
                table: "ApplyCertificateDeliveryForms");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplyCertificateDeliveryForms_CertificateDeliveryForms_Deli~",
                table: "ApplyCertificateDeliveryForms");

            migrationBuilder.DropIndex(
                name: "IX_ApplyCertificateDeliveryForms_DeliveryFormId1",
                table: "ApplyCertificateDeliveryForms");

            migrationBuilder.DropColumn(
                name: "DeliveryFormId1",
                table: "ApplyCertificateDeliveryForms");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyCertificateDeliveryForms_Applies_ApplyId",
                table: "ApplyCertificateDeliveryForms",
                column: "ApplyId",
                principalTable: "Applies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyCertificateDeliveryForms_CertificateDeliveryForms_Deli~",
                table: "ApplyCertificateDeliveryForms",
                column: "DeliveryFormId",
                principalTable: "CertificateDeliveryForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplyCertificateDeliveryForms_Applies_ApplyId",
                table: "ApplyCertificateDeliveryForms");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplyCertificateDeliveryForms_CertificateDeliveryForms_Deli~",
                table: "ApplyCertificateDeliveryForms");

            migrationBuilder.AddColumn<long>(
                name: "DeliveryFormId1",
                table: "ApplyCertificateDeliveryForms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ApplyCertificateDeliveryForms_DeliveryFormId1",
                table: "ApplyCertificateDeliveryForms",
                column: "DeliveryFormId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyCertificateDeliveryForms_Applies_DeliveryFormId",
                table: "ApplyCertificateDeliveryForms",
                column: "DeliveryFormId",
                principalTable: "Applies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplyCertificateDeliveryForms_CertificateDeliveryForms_Deli~",
                table: "ApplyCertificateDeliveryForms",
                column: "DeliveryFormId1",
                principalTable: "CertificateDeliveryForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
