using System.ComponentModel.DataAnnotations;


namespace MoviesAPI.Data.DTOs;

public class UpdateMovieDTO
{
    [Key]
    [Required]
    public Guid Id {get; set; }

    [Required(ErrorMessage = "The movie's title is mandatory!")]
    public string Title { get; set; }
    
    [Required(ErrorMessage = "The movie's genre is mandatory!")]
    [StringLength(50, ErrorMessage = "The size can't exceed 50 characters")]
    public string Genre { get; set; }

    [Required]
    [Range(70, 600, ErrorMessage = "The duration must have in 70 and 600 minutes")]
    public int Duration { get; set; }
}