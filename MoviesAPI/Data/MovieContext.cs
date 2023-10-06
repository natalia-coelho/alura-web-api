using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace MoviesAPI.Data;

public class MovieContext : DbContext
{
public class MovieContext : DbContext
{
    public MovieContext(DbContextOptions<MovieContext> options) : base(options){}

    //Propriedade Movie:
    public DbSet<Movie> Movies { get; set; }
}
}