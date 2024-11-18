namespace OOP

{
    public class Room
    {
            public int Id { get; set; }
            public string roomService { get; set; }

            // Name of the room, e.g., "Deluxe Suite"
            public string Name { get; set; }

            // Description of the room, detailing features and amenities
            public string Description { get; set; }

            // Room category, e.g., "Suite", "Standard", "Single"
            public string Category { get; set; }

            // Price per night for the room
            public double Price { get; set; }

            // Availability status of the room
            public bool IsAvailable { get; set; }
            public int Capacity { get; set; }
            public string Status { get; set; } // e.g., "Available", "Booked"
    }
  
}




