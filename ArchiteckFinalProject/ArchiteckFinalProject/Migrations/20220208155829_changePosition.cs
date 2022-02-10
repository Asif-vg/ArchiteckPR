using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiteckFinalProject.Migrations
{
    public partial class changePosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonPositions_Teams_TeamId",
                table: "PersonPositions");

            migrationBuilder.DropIndex(
                name: "IX_PersonPositions_TeamId",
                table: "PersonPositions");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "PersonPositions");

            migrationBuilder.AddColumn<int>(
                name: "PersonPositionId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PersonPositionId",
                table: "Teams",
                column: "PersonPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_PersonPositions_PersonPositionId",
                table: "Teams",
                column: "PersonPositionId",
                principalTable: "PersonPositions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_PersonPositions_PersonPositionId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_PersonPositionId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "PersonPositionId",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "PersonPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonPositions_TeamId",
                table: "PersonPositions",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonPositions_Teams_TeamId",
                table: "PersonPositions",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
