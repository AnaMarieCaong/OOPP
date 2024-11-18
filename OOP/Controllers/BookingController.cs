using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOP.Models;

namespace OOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private static List<Booking> bookings = new List<Booking>(); // In-memory list of bookings for demonstration

        // GET: api/booking
        [HttpGet]
        public IActionResult GetBookings()
        {
            return Ok(bookings);
        }

        // GET: api/booking/5
        [HttpGet("{id}")]
        public IActionResult GetBooking(int id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        // POST: api/booking
        [HttpPost]
        public IActionResult CreateBooking([FromBody] Booking booking)
        {
            if (booking == null || booking.CheckInDate >= booking.CheckOutDate)
            {
                return BadRequest("Invalid booking details.");
            }

            // Assign an ID (In a real app, this would come from the database)
            booking.Id = bookings.Count + 1;
            booking.Status = "Confirmed"; // Default status

            bookings.Add(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        // PUT: api/booking/5
        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, [FromBody] Booking updatedBooking)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            booking.RoomId = updatedBooking.RoomId;
            booking.UserId = updatedBooking.UserId;
            booking.CheckInDate = updatedBooking.CheckInDate;
            booking.CheckOutDate = updatedBooking.CheckOutDate;
            booking.Status = updatedBooking.Status;

            return NoContent();
        }

        // DELETE: api/booking/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            bookings.Remove(booking);
            return NoContent();
        }
    }
}
