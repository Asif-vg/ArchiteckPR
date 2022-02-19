using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiteckFinalProject.Migrations
{
    public partial class changedCustomUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "AspNetUsers",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: true);
        }
    }
}
