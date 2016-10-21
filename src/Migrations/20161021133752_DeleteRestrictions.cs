using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace src.Migrations
{
    public partial class DeleteRestrictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerOfMatchId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "GameId1",
                table: "Stats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stats_GameId1",
                table: "Stats",
                column: "GameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerId",
                table: "Games",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerOfMatchId",
                table: "Games",
                column: "PlayerOfMatchId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stats_Games_GameId1",
                table: "Stats",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_PlayerOfMatchId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Stats_Games_GameId1",
                table: "Stats");

            migrationBuilder.DropIndex(
                name: "IX_Stats_GameId1",
                table: "Stats");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "Stats");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerId",
                table: "Games",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_PlayerOfMatchId",
                table: "Games",
                column: "PlayerOfMatchId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
