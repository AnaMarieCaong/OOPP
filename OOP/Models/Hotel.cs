namespace OOP.Models
{
    public class Hotel
    {
        public int AvailableRooms { get; set; }
        public decimal PricePerNight { get; set; }
        public int HotelId { get; set; } // Foreign key for hotel
        public int HotelName { get; set; } // Foreign key for hotel

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Rating { get; set; } // e.g., star rating
        public int RoomCount { get; set; }
        // Add other properties as needed
    }
}
