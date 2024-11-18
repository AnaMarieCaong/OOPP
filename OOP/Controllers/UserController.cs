using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OOP.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace OOP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Static list to simulate a user database
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "ky", Email = "ky@example.com", Password = "ky" }  // Demo user
        };

        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // User login to generate JWT token
        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            // Find user by email and password
            var user = users.FirstOrDefault(u => u.Email == loginUser.Email && u.Password == loginUser.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate token for the authenticated user
            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

      
        // Generate JWT Token
        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Key"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Create a new user and generate JWT token upon signup
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            // Check if user already exists
            if (users.Any(u => u.Email == user.Email))
            {
                return BadRequest("User with this email already exists.");
            }

            // Auto-increment user ID
            user.Id = users.Count + 1;
            users.Add(user);

            // Generate token for the new user
            var token = GenerateJwtToken(user);

            // Return user data along with token
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new { User = user, Token = token });
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            return Ok(users);
        }

        // Get a user by ID
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // Update a user by ID
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            return NoContent();
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] User model)
        {
            // Code to register user in database (not shown here for brevity)
            return Ok("User registered successfully");
        }

        // Delete a user by ID
        [HttpDelete("{id}")]
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

        [Authorize]
        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            // Logout logic might involve client-side token disposal
            return Ok("Logout successful.");
        }
    }
}
