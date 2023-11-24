using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Data.DTOs;
public class ReadAddressDTO
{
    public int Id { get; set; }
    public string StreetName { get; set; }
    public int Number { get; set; }
}