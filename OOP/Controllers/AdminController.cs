using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOP.Models;

namespace OOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private static List<User> users = new List<User>();
        private static List<Room> rooms = new List<Room>();
        private static List<Booking> bookings = new List<Booking>();

        // Admin Actions for Users
        // GET: api/admin/users
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            return Ok(users);
        }

        // POST: api/admin/users
        [HttpPost("users")]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is invalid.");
            }

            user.Id = users.Count + 1;  // For demo purpose, auto-assign Id
            users.Add(user);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        // PUT: api/admin/users/{id}
        [HttpPut("users/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Role = updatedUser.Role;

            return NoContent();
        }

        // DELETE: api/admin/users/{id}
        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            users.Remove(user);
            return NoContent();
        }

        // Admin Actions for Rooms
        // GET: api/admin/rooms
        [HttpGet("rooms")]
        public IActionResult GetRooms()
        {
            return Ok(rooms);
        }

        // POST: api/admin/rooms
        [HttpPost("rooms")]
        public IActionResult CreateRoom([FromBody] Room room)
        {
            if (room == null)
            {
                return BadRequest("Room data is invalid.");
            }

            room.Id = rooms.Count + 1; // Auto-assign Id for demo purposes
            rooms.Add(room);
            return CreatedAtAction(nameof(GetRooms), new { id = room.Id }, room);
        }

        // PUT: api/admin/rooms/{id}
        [HttpPut("rooms/{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            var room = rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            room.Name = updatedRoom.Name;
            room.Capacity = updatedRoom.Capacity;
            room.Price = updatedRoom.Price;
            room.Status = updatedRoom.Status;

            return NoContent();
        }

        // DELETE: api/admin/rooms/{id}
        [HttpDelete("rooms/{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            rooms.Remove(room);
            return NoContent();
        }

        // Admin Actions for Bookings
        // GET: api/admin/bookings
        [HttpGet("bookings")]
        public IActionResult GetBookings()
        {
            return Ok(bookings);
        }

        // POST: api/admin/bookings
        [HttpPost("bookings")]
        public IActionResult CreateBooking([FromBody] Booking booking)
        {
            if (booking == null || booking.CheckInDate >= booking.CheckOutDate)
            {
                return BadRequest("Booking data is invalid.");
            }

            booking.Id = bookings.Count + 1; // Auto-assign Id for demo purposes
            booking.Status = "Confirmed"; // Default booking status
            bookings.Add(booking);
            return CreatedAtAction(nameof(GetBookings), new { id = booking.Id }, booking);
        }

        // PUT: api/admin/bookings/{id}
        [HttpPut("bookings/{id}")]
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

        // DELETE: api/admin/bookings/{id}
        [HttpDelete("bookings/{id}")]
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
