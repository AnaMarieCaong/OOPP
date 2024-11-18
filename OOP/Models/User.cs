namespace OOP.Models
{
    public class User
    {
        public int Id { get; set; }           // Unique identifier for the user
        public string Name { get; set; }       // Name of the user
        public string Email { get; set; }      // Email address of the user
        public string Password { get; set; }   // Password for the user account
        public string Role { get; set; } // e.g., "Admin", "Customer
    }
}
