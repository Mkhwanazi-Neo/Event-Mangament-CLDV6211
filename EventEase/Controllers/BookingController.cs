using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;



namespace EventEase.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================================
        // INDEX: Display all bookings + search feature
        // ============================================

        public async Task<IActionResult> Index(string searchString)
        {
            // Start with all records from the SQL view
            var query = _context.BookingDetailsView.AsQueryable();

            // Check if user entered a search value
            if (!string.IsNullOrEmpty(searchString))
            {
                // Try to convert the search text into an integer
                // If successful, search BookingID exactly
                if (int.TryParse(searchString, out int bookingId))
                {
                    query = query.Where(b => b.BookingId == bookingId);
                }
                else
                {
                    // If search text is not a number,
                    // search by Event Name using Contains
                    query = query.Where(b =>
                        b.EventName != null && b.EventName.Contains(searchString));
                }
            }

            // Execute query and send result to the view
            var bookings = await query.ToListAsync();

            // Keep search text in the textbox after search
            ViewBag.SearchString = searchString;

            return View(bookings);
        }

        public IActionResult Create()
        {
            ViewData["Events"] = _context.Event.ToList();
            ViewData["Venues"] = _context.Venue.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            var eventDate = _context.Event.FirstOrDefault(e => e.EventID == booking.EventID)?.EventDate;

            var conflict = await _context.Booking
                .AnyAsync(b => b.VenueID == booking.VenueID &&
                               _context.Event.Any(e =>
                                   e.EventID == b.EventID &&
                                   e.EventDate == eventDate));

            if (conflict)
            {
                ModelState.AddModelError("", "This venue is already booked for that date.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Events"] = _context.Event.ToList();
            ViewData["Venues"] = _context.Venue.ToList();
            return View(booking);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Booking
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingID == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}