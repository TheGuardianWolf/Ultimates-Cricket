using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ultimates_Cricket.Models;

namespace Ultimates_Cricket.Data
{
    public class DbInitialiser
    {
        public static void Initialize(Ultimates_CricketContext context)
        {
            

            // Look for any students.
            if (context.Players.Any())
            {
                return;   // DB has been seeded
            }
            context.Database.EnsureCreated();
            var players = new Player[]
            {
                new Player{Name="Tim Wackrow"},
            };
            foreach (Player p in players)
            {
                context.Players.Add(p);
            }
            context.SaveChanges();

            var games = new Game[]
            {
                new Game{ GameNumber=1, PlayerOfMatchId=1 },
            };
            foreach (Game g in games)
            {
                context.Games.Add(g);
            }
            context.SaveChanges();  

            var stats = new Stat[]
            {
                new Stat{PlayerId=1,GameId=1,Batting=50, Bowling=11},
            };
            foreach (Stat s in stats)
            {
                context.Stats.Add(s);
            }
            context.SaveChanges();
        }
    }
}
