using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Ultimates_Cricket.Data;

namespace src.Migrations
{
    [DbContext(typeof(Ultimates_CricketContext))]
    [Migration("20161021150319_Removed PlayerOfMatch Game Restriction")]
    partial class RemovedPlayerOfMatchGameRestriction
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ultimates_Cricket.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameNumber");

                    b.Property<int?>("PlayerId");

                    b.Property<int?>("PlayerOfMatchId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("PlayerOfMatchId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Ultimates_Cricket.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Photo")
                        .HasColumnType("varchar(MAX)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Ultimates_Cricket.Models.Stat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Batting");

                    b.Property<int>("Bowling");

                    b.Property<int>("GameId");

                    b.Property<int>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("Ultimates_Cricket.Models.Game", b =>
                {
                    b.HasOne("Ultimates_Cricket.Models.Player")
                        .WithMany("GamesWellPlayed")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Ultimates_Cricket.Models.Player", "PlayerOfMatch")
                        .WithMany()
                        .HasForeignKey("PlayerOfMatchId");
                });

            modelBuilder.Entity("Ultimates_Cricket.Models.Stat", b =>
                {
                    b.HasOne("Ultimates_Cricket.Models.Game", "Game")
                        .WithMany("Stats")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ultimates_Cricket.Models.Player", "Player")
                        .WithMany("Stats")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
