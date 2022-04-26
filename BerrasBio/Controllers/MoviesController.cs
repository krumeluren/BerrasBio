using BerrasBio.Data;
using BerrasBio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BerrasBio.Controllers
{
    public class MoviesController : Controller
    {
        private readonly BerrasBioContext _context;
        public MoviesController(BerrasBioContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movie.ToListAsync();

            return View(movies);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie movie = await _context.Movie
                .Include(a => a.Shows)
                    .ThenInclude(x => x.Bookable_Seats)
                        .ThenInclude(b => b.Booking)
                .SingleOrDefaultAsync(i => i.MovieID == id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

    }
}
