using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchiteckFinalProject.Migrations
{
    public partial class changedClientImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientImage",
                table: "Clients");

            migrationBuilder.CreateTable(
                name: "ClientImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientImages_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientImages_ClientId",
                table: "ClientImages",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientImages");

            migrationBuilder.AddColumn<string>(
                name: "ClientImage",
                table: "Clients",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }
    }
}
