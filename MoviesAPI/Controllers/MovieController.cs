using Microsoft.AspNetCore.Mvc;
using Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private static List<Movie> movieList = new List<Movie>();
    [HttpPost]
    public void AddMovie([FromBody] Movie movie)
    {
        movie.Id = Guid.NewGuid();
        movieList.Add(movie);
        Console.WriteLine(movie.Title);
        Console.WriteLine(movie.Genre);
        Console.WriteLine(movie.Duration);
    }

    [HttpGet("/")]
    public IEnumerable<Movie> RecoverMovies([FromQuery]int skip = 1, [FromQuery]int take = 4)
    {
        return movieList.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public Movie GetMovieById(Guid id)
    {
        return movieList.FirstOrDefault(movie => movie.Id == id);
    }
}