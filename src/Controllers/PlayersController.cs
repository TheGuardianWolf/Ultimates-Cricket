using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ultimates_Cricket.Models;
using Ultimates_Cricket.Data;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Ultimates_Cricket.Controllers
{
    public class PlayersController : Controller
    {
        private readonly Ultimates_CricketContext _context;

        public PlayersController(Ultimates_CricketContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            return View(await _context.Players.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players
                .Include(p => p.Stats)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (players == null)
            {
                return NotFound();
            }

            var battingScores = players.Stats
                .Select(s => s.Batting);

            players.BattingAverage = (battingScores.Count() > 0) ?
                (float)battingScores.Average() :
                0f;

            var catchesTaken = players.Stats
                .Select(s => s.Bowling);

            players.CatchesTaken = (catchesTaken.Count() > 0) ?
                catchesTaken.Sum() :
                0;

            return View(players);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Photo")] Player players)
        {
            if (ModelState.IsValid)
            {
                char[] seperators = { '/' };
                if (Request.Form.Files.Count > 0)
                {
                    if
                    (
                        Request.Form.Files[0].ContentType.Split(seperators, StringSplitOptions.RemoveEmptyEntries)[0] == "image" &&
                        Request.Form.Files[0].Length <= 204800
                    )
                    {
                        players.Photo = await ImageUploadToBase64(Request.Form.Files[0]);
                    }
                    else
                    {
                        ModelState.AddModelError("Photo", "Photo is not a valid image type or is over 200 KiB.");
                        return View(players);
                    }
                }

                _context.Add(players);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(players);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players.SingleOrDefaultAsync(m => m.Id == id);
            if (players == null)
            {
                return NotFound();
            }
            return View(players);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Photo")] Player players)
        {
            if (id != players.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                char[] seperators = { '/' };
                if (Request.Form.Files.Count > 0)
                {              
                    if
                    (
                        Request.Form.Files[0].ContentType.Split(seperators, StringSplitOptions.RemoveEmptyEntries)[0] == "image" &&
                        Request.Form.Files[0].Length <= 204800
                    )
                    {
                        players.Photo = await ImageUploadToBase64(Request.Form.Files[0]);
                    }
                    else
                    {
                        ModelState.AddModelError("Photo", "Photo is not a valid image type or is over 200 KiB.");
                        return View(players);
                    }
                }
                else
                {
                    players.Photo = await _context.Players.Where(p => p.Id == id).Select(p => p.Photo).SingleOrDefaultAsync();
                }

                try
                {
                    _context.Update(players);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayersExists(players.Id))
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
            return View(players);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players.SingleOrDefaultAsync(m => m.Id == id);
            if (players == null)
            {
                return NotFound();
            }

            return View(players);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var players = await _context.Players.SingleOrDefaultAsync(m => m.Id == id);
            _context.Players.Remove(players);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PlayersExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }

        private async Task<string> ImageUploadToBase64(IFormFile file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await Request.Form.Files[0].CopyToAsync(ms);
                return "data:" + Request.Form.Files[0].ContentType + ";base64," + Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
