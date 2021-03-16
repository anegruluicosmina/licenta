using Microsoft.EntityFrameworkCore.Migrations;

namespace licenta.Migrations
{
    public partial class AddedTestToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Tests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_ApplicationUserId",
                table: "Tests",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_AspNetUsers_ApplicationUserId",
                table: "Tests",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_AspNetUsers_ApplicationUserId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_ApplicationUserId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tests");
        }
    }
}
