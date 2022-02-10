using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiteckFinalProject.Migrations
{
    public partial class changedTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonSocials_Teams_TeamId",
                table: "PersonSocials");

            migrationBuilder.DropIndex(
                name: "IX_PersonSocials_TeamId",
                table: "PersonSocials");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "PersonSocials");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "PersonSocials",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonSocials_TeamId",
                table: "PersonSocials",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonSocials_Teams_TeamId",
                table: "PersonSocials",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
