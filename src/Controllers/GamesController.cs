using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ultimates_Cricket.Data;
using Ultimates_Cricket.Models;
using System.Collections;

namespace Ultimates_Cricket.Controllers
{
    public class GamesController : Controller
    {
        private readonly Ultimates_CricketContext _context;

        public GamesController(Ultimates_CricketContext context)
        {
            _context = context;    
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.Games.OrderBy(g => g.GameNumber).ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Stats)
                .ThenInclude(s => s.Player)    
                .Include(g => g.PlayerOfMatch)
                .SingleOrDefaultAsync(s => s.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            var playersList = _context.Players.ToList();
            playersList.Insert(0, new Player { Id = -1, Name = "Not Assigned" });
            ViewBag.PlayerOfMatchId = new SelectList(playersList, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameNumber,PlayerOfMatchId")] Game game)
        {
            if (game.PlayerOfMatchId == -1)
            {
                game.PlayerOfMatchId = null;
            }
            if (ModelState.IsValid)
            {
                if (!GameIsDuplicate(game.GameNumber))
                {
                    _context.Add(game);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("GameNumber", "The game already exists.");
            }
            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            } 

            var game = await _context.Games.SingleOrDefaultAsync(m => m.Id == id);

            if (game.PlayerOfMatchId == -1)
            {
                game.PlayerOfMatchId = null;
            }

            var playersList = await _context.Players.ToListAsync();
            playersList.Insert(0, new Player { Id = -1, Name = "Not Assigned" });
            ViewBag.PlayerOfMatchId = new SelectList(playersList, "Id", "Name");

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GameNumber,PlayerOfMatchId")] Game game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!GameIsDuplicate(game.Id, game.GameNumber))
                {
                    try
                    {
                        _context.Update(game);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!GameExists(game.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("GameNumber", "The game already exists.");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.SingleOrDefaultAsync(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.SingleOrDefaultAsync(m => m.Id == id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }

        private bool GameIsDuplicate(int gameNumber)
        {
            return _context.Games.Any(e => e.GameNumber == gameNumber);
        }

        private bool GameIsDuplicate(int id, int gameNumber)
        {
            return _context.Games.Any(e => (e.Id != id && e.GameNumber == gameNumber));
        }
    }
}
