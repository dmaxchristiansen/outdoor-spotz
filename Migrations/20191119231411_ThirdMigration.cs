using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "spots");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "spots",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "spots",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "County",
                table: "spots",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Directions",
                table: "spots",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "spots",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "County",
                table: "spots");

            migrationBuilder.DropColumn(
                name: "Directions",
                table: "spots");

            migrationBuilder.DropColumn(
                name: "State",
                table: "spots");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "spots",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "spots",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "spots",
                nullable: false,
                defaultValue: "");
        }
    }
}
