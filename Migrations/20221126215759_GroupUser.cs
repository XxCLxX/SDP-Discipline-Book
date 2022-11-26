using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp_book.Migrations
{
    public partial class GroupUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupsGroupId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupsGroupId",
                table: "AspNetUsers",
                column: "GroupsGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_GroupsGroupId",
                table: "AspNetUsers",
                column: "GroupsGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_GroupsGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupsGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupsGroupId",
                table: "AspNetUsers");
        }
    }
}
