using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_book.Migrations
{
    public partial class addClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayofClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeofClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeofClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildingofClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomofClass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
