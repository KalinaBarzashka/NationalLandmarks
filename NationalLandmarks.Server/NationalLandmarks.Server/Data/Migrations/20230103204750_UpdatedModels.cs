using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalLandmarks.Server.Data.Migrations
{
    public partial class UpdatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Closes",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "Opens",
                table: "Landmarks");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Visits");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Visits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Closes",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Opens",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
