using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiteckFinalProject.Migrations
{
    public partial class changedOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCatagories_Services_ServiceId",
                table: "ServiceCatagories");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCatagories_ServiceId",
                table: "ServiceCatagories");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ServiceCatagories");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Offices");

            migrationBuilder.RenameColumn(
                name: "Address2",
                table: "Offices",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_CategoryId",
                table: "Services",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceCatagories_CategoryId",
                table: "Services",
                column: "CategoryId",
                principalTable: "ServiceCatagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceCatagories_CategoryId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_CategoryId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Offices",
                newName: "Address2");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "ServiceCatagories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Offices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCatagories_ServiceId",
                table: "ServiceCatagories",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCatagories_Services_ServiceId",
                table: "ServiceCatagories",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
