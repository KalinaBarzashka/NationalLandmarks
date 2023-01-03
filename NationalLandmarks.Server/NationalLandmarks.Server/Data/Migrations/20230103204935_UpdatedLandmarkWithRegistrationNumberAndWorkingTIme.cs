using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalLandmarks.Server.Data.Migrations
{
    public partial class UpdatedLandmarkWithRegistrationNumberAndWorkingTIme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegistrationNumber",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingTime",
                table: "Landmarks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                table: "Landmarks");

            migrationBuilder.DropColumn(
                name: "WorkingTime",
                table: "Landmarks");
        }
    }
}
