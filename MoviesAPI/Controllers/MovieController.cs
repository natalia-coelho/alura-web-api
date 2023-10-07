using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private MovieContext _movieContext;
    private IMapper _mapper;
    
    public MovieController(MovieContext movieContext, IMapper mapper)
    {
        _movieContext = movieContext;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddMovie(
        [FromBody] CreateMovieDTO movieDto)
    {
        Movie movie = _mapper.Map<Movie>(movieDto);
        _movieContext.Movies.Add(movie);
        _movieContext.SaveChanges();
        return CreatedAtAction(nameof(GetMovieById),
            new { id = movie.Id }, 
            movie);
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
