using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using MoviesAPI.Data.DTOs;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private RegisterUserService _registerUserService;

        public UserController(RegisterUserService registerUserService)
        {
            _registerUserService = registerUserService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(CreateUserDTO userDTO)
        {    
            await _registerUserService.RegisterUser(userDTO);
            return Ok(userDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var user = _registerUserService.GetUsers();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(string id)
        {
            var user = _registerUserService.GetUserById(id);
            return Ok(user);
        }
    }
}
