namespace OOP.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int BookingId { get; set; } // Reference to the booking
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // e.g., "Credit Card", "PayPal"
        public string Status { get; set; } // e.g., "Pending", "Completed", "Failed"
        public DateTime PaymentDate { get; set; }
    }
}
