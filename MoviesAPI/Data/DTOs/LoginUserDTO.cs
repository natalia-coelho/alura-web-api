using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
