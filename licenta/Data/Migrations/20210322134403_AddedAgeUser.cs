using Microsoft.EntityFrameworkCore.Migrations;

namespace licenta.Migrations
{
    public partial class AddedAgeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_CategoryId",
                table: "Tests",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Categories_CategoryId",
                table: "Tests",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Categories_CategoryId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_CategoryId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");
        }
    }
}
