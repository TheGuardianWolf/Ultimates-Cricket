using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ultimates_Cricket.Models;
using Ultimates_Cricket.Data;

namespace Ultimates_Cricket.Controllers
{
    public class StatsController : Controller
    {
        private readonly Ultimates_CricketContext _context;

        public StatsController(Ultimates_CricketContext context)
        {
            _context = context;    
        }

        // GET: Stats
        private async Task<IActionResult> Index()
        {
            return View(await _context.Stats.ToListAsync());
        }

        // GET: Stats/Details/5
        private async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stats = await _context.Stats.SingleOrDefaultAsync(m => m.Id == id);
            if (stats == null)
            {
                return NotFound();
            }

            return View(stats);
        }

        // GET: Stats/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                ViewBag.GameId = new SelectList(_context.Games.ToList(), "Id", "GameNumber");
            }
            else
            {
                ViewBag.GameId = id;
            }
            ViewBag.PlayerId = new SelectList(_context.Players.ToList(), "Id", "Name");
            return View();
        }

        // POST: Stats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Batting,Bowling,GameId,PlayerId")] Stat stats)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stats);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Games", new { Id = stats.GameId });
            }
            return View(stats);
        }

        // GET: Stats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stats = await _context.Stats.SingleOrDefaultAsync(m => m.Id == id);

            if (stats == null)
            {
                return NotFound();
            }

            ViewBag.GameId = new SelectList(await _context.Games.ToListAsync(), "Id", "GameNumber", stats.GameId);

            return View(stats);
        }

        // POST: Stats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Batting,Bowling,GameId,PlayerId")] Stat stats)
        {
            if (id != stats.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatsExists(stats.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Games", new { Id = stats.GameId });
            }
            return View(stats);
        }

        // GET: Stats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stats = await _context.Stats.SingleOrDefaultAsync(m => m.Id == id);
            if (stats == null)
            {
                return NotFound();
            }

            return View(stats);
        }

        // POST: Stats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stats = await _context.Stats.SingleOrDefaultAsync(m => m.Id == id);
            _context.Stats.Remove(stats);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Games", new { Id = stats.GameId });
        }

        private bool StatsExists(int id)
        {
            return _context.Stats.Any(e => e.Id == id);
        }
    }
}
