using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Data.DTOs;
public class CreateCinemaDTO
{
    [Required(ErrorMessage = $"Field 'Name' is mandatory.")]
    public string Name { get; set; }
    public int AddressId { get; set; }
}
