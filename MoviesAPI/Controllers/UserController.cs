using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.DTOs;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var user = _userService.GetUsers();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(string id)
        {
            var user = _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUserDTO userDto)
        {
            var token = await _userService.LoginAsync(userDto);
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddUser(CreateUserDTO userDTO)
        {
            await _userService.RegisterUser(userDTO);
            return Ok(userDTO);
        }
    }
}
