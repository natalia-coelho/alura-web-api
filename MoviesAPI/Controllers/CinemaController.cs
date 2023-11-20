using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data;
using MoviesAPI.Data.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;
        public CinemaController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddCinema([FromBody] CreateCinemaDTO cinemaDTO)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDTO);
            _context.Cinemas.Add(cinema);

            return CreatedAtAction(nameof(GetCinemaById), new { Id = cinema.Id }, cinemaDTO);
        }

        [HttpGet]
        public IEnumerable<ReadCinemaDTO> GetCinema()
        {
            return _mapper.Map<List<ReadCinemaDTO>>(_context.Cinemas.ToList());
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

        [HttpDelete("id")]
        public IActionResult DeleteCinema(int id)
        {
            Cinema cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);
            if (cinema ==null)
                return NotFound();

            _context.Remove(cinema);
            _context.SaveChanges();

            return NoContent();
        }
    }
}