using Microsoft.AspNetCore.Mvc;
using Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private static List<Movie> movieList = new List<Movie>();

    [HttpPost]
    public IActionResult AddMovie([FromBody] Movie movie)
    {
        movie.Id = Guid.NewGuid();
        movieList.Add(movie);
        return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
    }

    [HttpGet("/")]
    public IEnumerable<Movie> RecoverMovies([FromQuery]int skip = 0, [FromQuery]int take = 2    )
    {
        return movieList.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(Guid id)
    {
        var checkMovie =  movieList.FirstOrDefault(movie => movie.Id == id);
        if (checkMovie == null) return NotFound();
        return Ok(checkMovie);
    }
}
