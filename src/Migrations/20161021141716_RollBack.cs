using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace src.Migrations
{
    public partial class RollBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Games_GameId1",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_GameId1",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "Stats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId1",
                table: "Stats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stats_GameId1",
                table: "Stats",
                column: "GameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Games_GameId1",
                table: "Stats",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
