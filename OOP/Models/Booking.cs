namespace OOP.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; } // e.g., "Confirmed", "Cancelled", etc.
    }
}
