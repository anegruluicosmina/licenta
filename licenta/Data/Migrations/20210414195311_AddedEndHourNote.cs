using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace licenta.Migrations
{
    public partial class AddedEndHourNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hour",
                table: "Notes",
                newName: "StartHour");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndHour",
                table: "Notes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "StartHour",
                table: "Notes",
                newName: "Hour");
        }
    }
}
