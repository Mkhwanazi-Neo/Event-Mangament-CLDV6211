using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchType, int venueID, int eventTypeID)
        {   
            var events = _context.Event
            .Include(e => e.Venue)
            .Include(e => e.EventType)
            .AsQueryable();

            if (!string.IsNullOrEmpty(searchType))
            {
                events = events.Where(e => e.EventName.Contains(searchType));
            }

            if (venueID != 0)
            {
                events = events.Where(e => e.VenueID == venueID);
            }

            if (eventTypeID != 0)
            {
                events = events.Where(e => e.EventTypeID == eventTypeID);
            }

            ViewData["Venues"] = _context.Venue.ToList();
            ViewData["EventTypes"] = _context.EventType.ToList();

            return View(await events.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["Venues"] = _context.Venue.ToList();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Venues"] = _context.Venue.ToList();
            return View(@event);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Event.FindAsync(id);
            if (@event == null) return NotFound();

            ViewData["Venues"] = _context.Venue.ToList();
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event @event)
        {
            if (id != @event.EventID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Venues"] = _context.Venue.ToList();
            return View(@event);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Event.FindAsync(id);
            if (@event == null) return NotFound();

            var isBooked = await _context.Booking.AnyAsync(b => b.EventID == id);
            if (isBooked)
            {
                TempData["ErrorMessage"] = "Cannot delete event with existing bookings.";
                return View("Index", await _context.Event.Include(e => e.Venue).ToListAsync());
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(e => e.Venue) // Include related venue if applicable
                .FirstOrDefaultAsync(m => m.EventID == id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }
    }
}
