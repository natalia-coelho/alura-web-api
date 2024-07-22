using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}
