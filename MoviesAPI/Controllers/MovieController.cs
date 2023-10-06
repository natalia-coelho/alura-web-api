using Microsoft.AspNetCore.Mvc;
using Models;
using MoviesAPI.Data;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private MovieContext _movieContext;

    public MovieController(MovieContext movieContext)
    {
        _movieContext = movieContext;
    }

    [HttpPost]
    public IActionResult AddMovie([FromBody] Movie movie)
    {
        _movieContext.Movies.Add(movie);
        _movieContext.SaveChanges();
        return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
    }

    [HttpGet("/")]
    public IEnumerable<Movie> RecoverMovies([FromQuery]int skip = 0, [FromQuery]int take = 2)
    {
        return _movieContext.Movies.Skip(skip).Take(take);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(Guid id)
    {
        var movie =  _movieContext.Movies.FirstOrDefault(movie => movie.Id == id);
        if (movie == null) return NotFound();
        return Ok(movie);
    }
}
