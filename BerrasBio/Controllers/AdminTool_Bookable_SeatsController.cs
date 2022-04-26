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
    public class AdminTool_Bookable_SeatsController : Controller
    {
        private readonly BerrasBioContext _context;

        public AdminTool_Bookable_SeatsController(BerrasBioContext context)
        {
            _context = context;
        }

        // GET: AdminTool_Bookable_Seats
        public async Task<IActionResult> Index()
        {
            var berrasBioContext = _context.Bookable_Seats.Include(b => b.Booking).Include(b => b.Seat).Include(b => b.Show);
            return View(await berrasBioContext.ToListAsync());
        }

        // GET: AdminTool_Bookable_Seats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookable_Seats = await _context.Bookable_Seats
                .Include(b => b.Booking)
                .Include(b => b.Seat)
                .Include(b => b.Show)
                .FirstOrDefaultAsync(m => m.Bookable_SeatsID == id);
            if (bookable_Seats == null)
            {
                return NotFound();
            }

            return View(bookable_Seats);
        }

        // GET: AdminTool_Bookable_Seats/Create
        public IActionResult Create()
        {
            ViewData["BookingID"] = new SelectList(_context.Set<Booking>(), "BookingID", "BookingID");
            ViewData["SeatID"] = new SelectList(_context.Set<Seat>(), "SeatID", "SeatID");
            ViewData["ShowID"] = new SelectList(_context.Show, "ShowID", "ShowID");
            return View();
        }

        // POST: AdminTool_Bookable_Seats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bookable_SeatsID,Ticket_Price,ShowID,SeatID,BookingID")] Bookable_Seats bookable_Seats)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookable_Seats);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingID"] = new SelectList(_context.Set<Booking>(), "BookingID", "BookingID", bookable_Seats.BookingID);
            ViewData["SeatID"] = new SelectList(_context.Set<Seat>(), "SeatID", "SeatID", bookable_Seats.SeatID);
            ViewData["ShowID"] = new SelectList(_context.Show, "ShowID", "ShowID", bookable_Seats.ShowID);
            return View(bookable_Seats);
        }

        // GET: AdminTool_Bookable_Seats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookable_Seats = await _context.Bookable_Seats.FindAsync(id);
            if (bookable_Seats == null)
            {
                return NotFound();
            }
            ViewData["BookingID"] = new SelectList(_context.Set<Booking>(), "BookingID", "BookingID", bookable_Seats.BookingID);
            ViewData["SeatID"] = new SelectList(_context.Set<Seat>(), "SeatID", "SeatID", bookable_Seats.SeatID);
            ViewData["ShowID"] = new SelectList(_context.Show, "ShowID", "ShowID", bookable_Seats.ShowID);
            return View(bookable_Seats);
        }

        // POST: AdminTool_Bookable_Seats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Bookable_SeatsID,Ticket_Price,ShowID,SeatID,BookingID")] Bookable_Seats bookable_Seats)
        {
            if (id != bookable_Seats.Bookable_SeatsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookable_Seats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Bookable_SeatsExists(bookable_Seats.Bookable_SeatsID))
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
            ViewData["BookingID"] = new SelectList(_context.Set<Booking>(), "BookingID", "BookingID", bookable_Seats.BookingID);
            ViewData["SeatID"] = new SelectList(_context.Set<Seat>(), "SeatID", "SeatID", bookable_Seats.SeatID);
            ViewData["ShowID"] = new SelectList(_context.Show, "ShowID", "ShowID", bookable_Seats.ShowID);
            return View(bookable_Seats);
        }

        // GET: AdminTool_Bookable_Seats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookable_Seats = await _context.Bookable_Seats
                .Include(b => b.Booking)
                .Include(b => b.Seat)
                .Include(b => b.Show)
                .FirstOrDefaultAsync(m => m.Bookable_SeatsID == id);
            if (bookable_Seats == null)
            {
                return NotFound();
            }

            return View(bookable_Seats);
        }

        // POST: AdminTool_Bookable_Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookable_Seats = await _context.Bookable_Seats.FindAsync(id);
            _context.Bookable_Seats.Remove(bookable_Seats);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Bookable_SeatsExists(int id)
        {
            return _context.Bookable_Seats.Any(e => e.Bookable_SeatsID == id);
        }
    }
}
