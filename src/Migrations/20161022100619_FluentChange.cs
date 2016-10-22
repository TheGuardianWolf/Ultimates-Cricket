using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace src.Migrations
{
    public partial class FluentChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerOfMatchId",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerOfMatchId",
                table: "Games",
                column: "PlayerOfMatchId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerOfMatchId",
                table: "Games");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerOfMatchId",
                table: "Games",
                column: "PlayerOfMatchId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
