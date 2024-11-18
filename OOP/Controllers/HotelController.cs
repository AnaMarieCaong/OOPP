using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOP.Models;

namespace OOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private static List<Hotel> hotels = new List<Hotel>();
        private static int nextId = 1;

        // GET: api/hotel
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> GetHotels()
        {
            return Ok(hotels);
        }

        // GET: api/hotel/{id}
        [HttpGet("{id}")]
        public ActionResult<Hotel> GetHotel(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound("Hotel not found.");
            }
            return Ok(hotel);
        }

        // POST: api/hotel
        [HttpPost]
        public ActionResult<Hotel> CreateHotel([FromBody] Hotel hotel)
        {
            // Input validation
            if (hotel == null)
            {
                return BadRequest("Hotel information is required.");
            }

            // Setting the ID and adding to the list
            hotel.Id = nextId++;
            hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }

        // PUT: api/hotel/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateHotel(int id, [FromBody] Hotel updatedHotel)
        {
            // Check if the hotel exists
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound("Hotel not found.");
            }

            // Update properties
            hotel.Name = updatedHotel.Name;
            hotel.Location = updatedHotel.Location;
            hotel.Rating = updatedHotel.Rating;
            hotel.RoomCount = updatedHotel.RoomCount;
            // Update other properties as needed

            return NoContent(); // Return 204 No Content on success
        }

        // DELETE: api/hotel/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteHotel(int id)
        {
            // Check if the hotel exists
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null)
            {
                return NotFound("Hotel not found.");
            }

            hotels.Remove(hotel);
            return NoContent(); // Return 204 No Conte
        }
    }
}
