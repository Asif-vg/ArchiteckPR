using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiteckFinalProject.Migrations
{
    public partial class changedProjectArchiteck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectArchitecks_ProjectArchitecks_ProjectArchiteckId",
                table: "ProjectArchitecks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectArchitecks_ProjectArchiteckId",
                table: "ProjectArchitecks");

            migrationBuilder.DropColumn(
                name: "ProjectArchiteckId",
                table: "ProjectArchitecks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectArchiteckId",
                table: "ProjectArchitecks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectArchitecks_ProjectArchiteckId",
                table: "ProjectArchitecks",
                column: "ProjectArchiteckId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectArchitecks_ProjectArchitecks_ProjectArchiteckId",
                table: "ProjectArchitecks",
                column: "ProjectArchiteckId",
                principalTable: "ProjectArchitecks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
