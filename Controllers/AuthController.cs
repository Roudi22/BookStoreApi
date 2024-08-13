using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly TokenService _tokenService;

        public AuthController(UserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { Message = "Username and password are required." });
            }

            if (_userRepository.GetAll().Any(u => u.Username == user.Username))
            {
                return BadRequest(new { Message = "Username already exists." });
            }

            var newUser = _userRepository.Register(user);
            return Ok(new { Message = "User registered successfully.", User = newUser });
        }
        
        // get all users
        [HttpGet("users")]
        public IActionResult GetAll()
        {
            var users = _userRepository.GetAll();
            return Ok(new { Message = "Request successful.", Users = users });
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { Message = "Username and password are required." });
            }

            var authenticatedUser = _userRepository.Authenticate(user.Username, user.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            var token = _tokenService.GenerateToken(authenticatedUser);
            return Ok(new { Message = "Login successful.", Token = token });
        }
    }
}
