using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Data.DTOs;
public class UpdateCinemaDTO
{
    [Required(ErrorMessage = $"Field 'Name' is mandatory.")]
    public string Name { get; set; }
}