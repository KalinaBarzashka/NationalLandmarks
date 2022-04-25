namespace NationalLandmarks.Server.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UpdatedLandmarkModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasSeal",
                table: "Landmarks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasSeal",
                table: "Landmarks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
