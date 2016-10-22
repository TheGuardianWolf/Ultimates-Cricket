using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ultimates_Cricket.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ultimates_Cricket.Data
{
    public class Ultimates_CricketContext : DbContext
    {
        public Ultimates_CricketContext(DbContextOptions<Ultimates_CricketContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Player>()
            //.HasMany(p => p.GamesWellPlayed)
            //.WithOne()
            //.OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Player>()
            .Ignore(p => p.BattingAverage);

            modelBuilder.Entity<Player>()
            .Ignore(p => p.CatchesTaken);

            modelBuilder.Entity<Player>()
            .HasMany(p => p.GamesWellPlayed)
            .WithOne(g => g.PlayerOfMatch)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Game>()
            .HasOne(g => g.PlayerOfMatch)
            .WithMany(p => p.GamesWellPlayed)
            .HasForeignKey(g => g.PlayerOfMatchId);

            //modelBuilder.Entity<Game>()
            //.HasOne(g => g.PlayerOfMatch)
            //.WithMany()
            //.OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Stat>()
            //.HasOne(s => s.Game)
            //.WithMany()
            //.OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Stat> Stats { get; set; }

        public DbSet<Game> Games { get; set; }
    }
}
