#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Data;
using BerrasBio.Models;

namespace BerrasBio.Controllers
{

    public class AdminTool_ShowsController : Controller
    {
        private readonly BerrasBioContext _context;

        public AdminTool_ShowsController(BerrasBioContext context)
        {
            _context = context;
        }

        // GET: AdminTool_Shows
        public async Task<IActionResult> Index()
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            var berrasBioContext = _context.Show
                .Include(s => s.Movie)
                .Include(s => s.Saloon)
                .Include(s => s.Bookable_Seats);
            return View(await berrasBioContext.ToListAsync());
        }

        // GET: AdminTool_Shows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .Include(s => s.Movie)
                .Include(s => s.Saloon)
                    .ThenInclude(s => s.Seats)
                .Include(s => s.Bookable_Seats)
                .FirstOrDefaultAsync(m => m.ShowID == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // GET: AdminTool_Shows/Create
        public IActionResult Create()
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            //create a list of movies to be used in the dropdown
            var movies = _context.Movie.ToList();
            //create a list of saloons to be used in the dropdown
            var saloons = _context.Saloon.ToList();
            //Create a selectlist of movies
            var movieSelectList = new SelectList(movies, "MovieID", "Title");
            //Create a selectlist of saloons
            var saloonSelectList = new SelectList(saloons, "SaloonID", "Name");

            ViewData["MovieID"] = movieSelectList;
            ViewData["SaloonID"] = saloonSelectList;
            return View();
        }

        // POST: AdminTool_Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShowID,ShowTime,MovieID,SaloonID")] Show show)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            if (ModelState.IsValid)
            {
                _context.Add(show);
                
                await _context.SaveChangesAsync();

                show = await _context.Show
                    .Include(s => s.Movie)
                    .Include(s => s.Saloon)
                        .ThenInclude(s => s.Seats)
                    .FirstOrDefaultAsync(m => m.ShowID == show.ShowID);

                
                foreach (Seat seat in show.Saloon.Seats)
                {
                    Bookable_Seats bookable_Seats = new Bookable_Seats();
                    bookable_Seats.SeatID = seat.SeatID;
                    bookable_Seats.ShowID = show.ShowID;
                    _context.Add(bookable_Seats);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieID", show.MovieID);
            ViewData["SaloonID"] = new SelectList(_context.Set<Saloon>(), "SaloonID", "SaloonID", show.SaloonID);
            return View(show);
        }

        // GET: AdminTool_Shows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Show.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieID", show.MovieID);
            ViewData["SaloonID"] = new SelectList(_context.Set<Saloon>(), "SaloonID", "SaloonID", show.SaloonID);
            return View(show);
        }

        // POST: AdminTool_Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShowID,ShowTime,MovieID,SaloonID")] Show show)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            if (id != show.ShowID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(show);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowExists(show.ShowID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieID"] = new SelectList(_context.Movie, "MovieID", "MovieID", show.MovieID);
            ViewData["SaloonID"] = new SelectList(_context.Set<Saloon>(), "SaloonID", "SaloonID", show.SaloonID);
            return View(show);
        }

        // GET: AdminTool_Shows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");
            
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .Include(s => s.Movie)
                .Include(s => s.Saloon)
                .FirstOrDefaultAsync(m => m.ShowID == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // POST: AdminTool_Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            var show = await _context.Show.FindAsync(id);
            _context.Show.Remove(show);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowExists(int id)
        {
            return _context.Show.Any(e => e.ShowID == id);
        }
    }
}
