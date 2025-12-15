using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OtomasyonApi.Repositories;
using OtomasyonApi.Models;
using BCrypt.Net;


namespace OtomasyonApi.Controllers
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        public UsersController(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _jwtKey = config["Jwt:Key"];
            _jwtIssuer = config["Jwt:Issuer"];
            _jwtAudience = config["Jwt:Audience"];
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.username) || string.IsNullOrWhiteSpace(request.password))
                return BadRequest("Username or password missing");

            var user = await _userRepository.GetByUsernameAsync(request.username);

            if (user == null)
                return Unauthorized("User not found");

            // BCrypt doğrulama
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.password, user.PasswordHash);

            if (!isPasswordValid)
                return Unauthorized("Wrong password");

            // JWT üret
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                Issuer = _jwtIssuer,
                Audience = _jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                username = user.Username,
                fullname = user.FullName,
                role = user.Role
            });
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.username) || string.IsNullOrWhiteSpace(request.password) || string.IsNullOrWhiteSpace(request.fullname))
                return BadRequest("Username, password, or fullname missing");

            var existingUser = await _userRepository.GetByUsernameAsync(request.username);
            if (existingUser != null)
                return BadRequest("Username already exists");

            // Şifreyi hash'le
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.password);

            var newUser = new User
            {
                Username = request.username,
                FullName = request.fullname,
                PasswordHash = hashedPassword,
                Role = "User",
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(newUser);

            return Created("", new
            {
                id = newUser.Id,
                username = newUser.Username,
                fullname = newUser.FullName,
                role = newUser.Role
            });
        }
    }

    public class RegisterRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string fullname { get; set; }
    }
}
