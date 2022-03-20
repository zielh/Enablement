using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleRestAPI2.DAL.Migrations
{
    public partial class UpdateAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UsersId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UsersId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Roles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UsersId",
                table: "Roles",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UsersId",
                table: "Roles",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
