using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NationalLandmarks.Server.Data.Migrations
{
    public partial class LandmarkDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Landmark_AspNetUsers_UserId",
                table: "Landmark");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Landmark",
                table: "Landmark");

            migrationBuilder.RenameTable(
                name: "Landmark",
                newName: "Landmarks");

            migrationBuilder.RenameIndex(
                name: "IX_Landmark_UserId",
                table: "Landmarks",
                newName: "IX_Landmarks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Landmarks",
                table: "Landmarks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Landmarks_AspNetUsers_UserId",
                table: "Landmarks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Landmarks_AspNetUsers_UserId",
                table: "Landmarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Landmarks",
                table: "Landmarks");

            migrationBuilder.RenameTable(
                name: "Landmarks",
                newName: "Landmark");

            migrationBuilder.RenameIndex(
                name: "IX_Landmarks_UserId",
                table: "Landmark",
                newName: "IX_Landmark_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Landmark",
                table: "Landmark",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Landmark_AspNetUsers_UserId",
                table: "Landmark",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
