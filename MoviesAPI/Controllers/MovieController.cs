using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    public IEnumerable<ReadMovieDTO> RecoverMovies([FromQuery]int skip = 0, [FromQuery]int take = 50)
    {
        return _mapper.Map<List<ReadMovieDTO>>(_movieContext.Movies.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(Guid id)
    {
        var existingMovie =  _movieContext.Movies.FirstOrDefault(movie => movie.Id == id);
        if (existingMovie == null) return NotFound();
        var movieDTO = _mapper.Map<ReadMovieDTO>(existingMovie);
        return Ok(existingMovie);
    }
    [HttpPut("{id}")]
    public IActionResult UpdateMovie(Guid id, [FromBody] UpdateMovieDTO movieDTO) 
    { 
        var existingMovie = _movieContext.Movies.FirstOrDefault(movie => movie.Id == id);

        if (existingMovie == null) 
        {
            return NotFound();
        }

        existingMovie.Title = movieDTO.Title;
        existingMovie.Genre = movieDTO.Genre;
        existingMovie.Duration = movieDTO.Duration;

        _movieContext.SaveChanges();

        _movieContext.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult UpdateMoviePatch(Guid id, JsonPatchDocument<UpdateMovieDTO> patch) 
    { 
        var existingMovie = _movieContext.Movies.FirstOrDefault(
            movie => movie.Id == id);
        if (existingMovie == null) return NotFound();

        var updatedMovie = _mapper.Map<UpdateMovieDTO>(existingMovie);

        patch.ApplyTo(updatedMovie, ModelState);

        if(!TryValidateModel(updatedMovie))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(updatedMovie, existingMovie);
        _movieContext.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(Guid id)
    { 
        var existingMovie = _movieContext.Movies.FirstOrDefault(
            movie => movie.Id == id);
        if (existingMovie == null) return NotFound();
        _movieContext.Remove(existingMovie);
        _movieContext.SaveChanges();
        return NoContent();
    }
}
