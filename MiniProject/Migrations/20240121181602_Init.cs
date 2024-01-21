using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniProject.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Intrests",
                columns: table => new
                {
                    IntrestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intrests", x => x.IntrestId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "PersonIntrests",
                columns: table => new
                {
                    PersonIntrestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    IntrestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonIntrests", x => x.PersonIntrestId);
                    table.ForeignKey(
                        name: "FK_PersonIntrests_Intrests_IntrestId",
                        column: x => x.IntrestId,
                        principalTable: "Intrests",
                        principalColumn: "IntrestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonIntrests_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    LinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonIntrestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_Links_PersonIntrests_PersonIntrestId",
                        column: x => x.PersonIntrestId,
                        principalTable: "PersonIntrests",
                        principalColumn: "PersonIntrestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_PersonIntrestId",
                table: "Links",
                column: "PersonIntrestId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonIntrests_IntrestId",
                table: "PersonIntrests",
                column: "IntrestId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonIntrests_PersonId",
                table: "PersonIntrests",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "PersonIntrests");

            migrationBuilder.DropTable(
                name: "Intrests");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
