using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Data.DTOs
{
    public class ReadMovieDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public DateTime LastReadTime { get; set; } = DateTime.Now;
    }
}