
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOP.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OOP .Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private static List<Room> _rooms = new List<Room>()
        {
            new Room { Id = 1, Name = "Deluxe Suite", Description = "Spacious room with ocean view", Category = "Suite", Price = 200.00, IsAvailable = true },
            new Room { Id = 2, Name = "Standard Room", Description = "Comfortable room with city view", Category = "Standard", Price = 100.00, IsAvailable = true },
            new Room { Id = 3, Name = "Single Room", Description = "Cozy room for one", Category = "Single", Price = 80.00, IsAvailable = false }
            // Add more rooms as needed
        };


        // Search rooms by ID or name
        [HttpGet("search")]
        public ActionResult<List<Room>> SearchRooms([FromQuery] string term)
        {
            int roomId;
            bool isId = int.TryParse(term, out roomId);

            var rooms = _rooms
                .Where(r => (isId && r.Id == roomId) ||
                            r.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            r.Description.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            if (!rooms.Any())
            {
                return NotFound("No rooms found.");
            }

            return Ok(rooms);
        }

        // Get all rooms
        [HttpGet]
        public ActionResult<List<Room>> Get()
        {
            return Ok(_rooms);
        }

        // Get a specific room by ID
        [HttpGet("{id}")]
        public ActionResult<Room> Get(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        // Get rooms by category
        [HttpGet("category/{category}")]
        public ActionResult<List<Room>> GetByCategory(string category)
        {
            var categoryRooms = _rooms.Where(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!categoryRooms.Any())
            {
                return NotFound();
            }
            return Ok(categoryRooms);
        }

        // Get available or unavailable rooms
        [HttpGet("availability/{isAvailable}")]
        public ActionResult<List<Room>> GetByAvailability(bool isAvailable)
        {
            var availabilityRooms = _rooms.Where(r => r.IsAvailable == isAvailable).ToList();
            if (!availabilityRooms.Any())
            {
                return NotFound();
            }
            return Ok(availabilityRooms);
        }

        // Add a new room
        [HttpPost]
     

        // Update an existing room
        [HttpPut("{id}")]
        public ActionResult<Room> Put(int id, [FromBody] Room updatedRoom)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            room.Name = updatedRoom.Name;
            room.Description = updatedRoom.Description;
            room.Category = updatedRoom.Category;
            room.Price = updatedRoom.Price;
            room.IsAvailable = updatedRoom.IsAvailable;

            return NoContent();
        }

        // Delete a room
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            _rooms.Remove(room);
            return NoContent();
        }
    }
}
