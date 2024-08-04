using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class AccessController : ControllerBase
    {
        // a ideia desse controlador é validar o acesso a partir do token 
        [HttpGet]
        [Authorize(Policy = "IdadeMinima")] // valida se o token é válido
        public IActionResult Get() 
        {
            return Ok("Acesso permitido");
        }
    }
}
