using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CinemaController : ControllerBase
{
    private MovieDbContext _context;
    private IMapper _mapper;
    public CinemaController(MovieDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AddCinema([FromBody] CreateCinemaDTO cinemaDTO)
    {
        Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);
        _context.Cinemas.Add(cinema);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetCinemaById), new { Id = cinema.Id }, cinemaDTO);
    }

    [HttpGet]
    public IEnumerable<ReadCinemaDTO> GetCinema([FromQuery] int? enderecoId = null)
    {
            if(enderecoId == null)
            {
                return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.ToList());
            }
            return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.FromSqlRaw($"SELECT Id, Name, AddressId FROM cinemas where cinemas.addressId = {enderecoId}").ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetCinemaById(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema != null)
        {
            ReadCinemaDTO readCinemaDTO = _mapper.Map<ReadCinemaDTO>(cinema);
            return Ok(readCinemaDTO);
        }
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCinema(int id, [FromBody] UpdateCinemaDTO updateCinemaDTO)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null)
            return NotFound();

        _mapper.Map(updateCinemaDTO, cinema);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCinema(int id)
    {
        Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
        if (cinema == null)
            return NotFound();

        _context.Remove(cinema);
        _context.SaveChanges();

        return NoContent();
    }
}