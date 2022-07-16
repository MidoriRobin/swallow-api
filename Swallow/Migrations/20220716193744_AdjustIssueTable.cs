using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swallow.Migrations
{
    public partial class AdjustIssueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Issues");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedEnd",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpectedEnd",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "Issues",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorUserId",
                table: "Issues",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
