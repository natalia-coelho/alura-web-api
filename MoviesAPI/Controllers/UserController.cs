using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.DTOs;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddUser(CreateUserDTO userDTO)
        {
            throw new NotImplementedException();
        }
    }
}
