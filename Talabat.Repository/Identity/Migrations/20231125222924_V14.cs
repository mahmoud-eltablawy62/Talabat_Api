using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Identity.Migrations
{
    public partial class V14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Adress",
                table: "Adress");

            migrationBuilder.RenameTable(
                name: "Adress",
                newName: "Adresses");

            migrationBuilder.AddColumn<int>(
                name: "AdressId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AdressId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdressId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.DropColumn(
                name: "AdressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Adresses",
                newName: "Adress");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adress",
                table: "Adress",
                column: "Id");
        }
    }
}
