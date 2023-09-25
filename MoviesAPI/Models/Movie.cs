using System.ComponentModel.DataAnnotations;

namespace Models;
public class Movie
{
    [Key]
    [Required]
    public Guid Id {get; set; }

    [Required(ErrorMessage = "Movie's title is mandatory!")]
    public string? Title { get; set; }
    
    [Required(ErrorMessage = "Movie's genre is mandatory!")]
    public string? Genre { get; set; }

    [Required]
    public int? Duration { get; set; }
}
    