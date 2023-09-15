using System.ComponentModel.DataAnnotations;

namespace Models;
public class Movie
{
    public Guid Id {get; set; }

    [Required]
    public string? Title { get; set; }
    
    [Required]
    public string? Genre { get; set; }

    [Required]
    public int? Duration { get; set; }
}
    