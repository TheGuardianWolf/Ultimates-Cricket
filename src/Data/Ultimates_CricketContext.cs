﻿using System;
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
        public Ultimates_CricketContext (DbContextOptions<Ultimates_CricketContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
            .HasMany(p => p.GamesWellPlayed)
            .WithOne()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
            .HasOne(p => p.PlayerOfMatch)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Stat> Stats { get; set; }

        public DbSet<Game> Games { get; set; }
    }
}