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
        movieList.Add(movie);
        Console.WriteLine(movie.Title);
        Console.WriteLine(movie.Genre);
        Console.WriteLine(movie.Duration);
    }

    [HttpGet("/")]
    public List<Movie> GetMovies ()
    {
        return movieList;
    }

    [HttpGet("/{$id}")]
    public Movie GetMovieById(Movie movie)
    {
        return movie;
    }
}