using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Identity.Migrations
{
    public partial class V12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AdressId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AdressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Adresses");

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Adresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_UsersId",
                table: "Adresses",
                column: "UsersId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_AspNetUsers_UsersId",
                table: "Adresses",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_AspNetUsers_UsersId",
                table: "Adresses");

            migrationBuilder.DropIndex(
                name: "IX_Adresses_UsersId",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Adresses");

            migrationBuilder.AddColumn<int>(
                name: "AdressId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdressId",
                table: "AspNetUsers",
                column: "AdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresses_AdressId",
                table: "AspNetUsers",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
