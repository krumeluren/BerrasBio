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
    public class AdminTool_MoviesController : Controller
    {
        private readonly BerrasBioContext _context;

        public AdminTool_MoviesController(BerrasBioContext context)
        {
            _context = context;
        }



        // GET: AdminTool_Movies
        public async Task<IActionResult> Index()
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");            
            
            return View(await _context.Movie.ToListAsync());
        }

        // GET: AdminTool_Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");
            
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: AdminTool_Movies/Create
        public IActionResult Create()
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");
            
            return View();
        }

        // POST: AdminTool_Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieID,Title,Length,Image_Source")] Movie movie)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");
            
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: AdminTool_Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: AdminTool_Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieID,Title,Length,Image_Source")] Movie movie)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            if (id != movie.MovieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieID))
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
            return View(movie);
        }

        // GET: AdminTool_Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.MovieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: AdminTool_Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Admin role check
            if (!User.IsInRole("Administrator")) return RedirectToAction(nameof(StartController.Index), "Start");

            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieID == id);
        }
    }
}
