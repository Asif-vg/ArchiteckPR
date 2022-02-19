using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiteckFinalProject.Migrations
{
    public partial class addProjectArchiteck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Architeck",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectArchiteckId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectArchitecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProjectArchiteckId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectArchitecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectArchitecks_ProjectArchitecks_ProjectArchiteckId",
                        column: x => x.ProjectArchiteckId,
                        principalTable: "ProjectArchitecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectArchiteckId",
                table: "Projects",
                column: "ProjectArchiteckId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectArchitecks_ProjectArchiteckId",
                table: "ProjectArchitecks",
                column: "ProjectArchiteckId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectArchitecks_ProjectArchiteckId",
                table: "Projects",
                column: "ProjectArchiteckId",
                principalTable: "ProjectArchitecks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectArchitecks_ProjectArchiteckId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectArchitecks");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectArchiteckId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectArchiteckId",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "Architeck",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
