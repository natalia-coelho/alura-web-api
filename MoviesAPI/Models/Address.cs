using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models;
public class Address
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string StreetName { get; set; }
    public int Number { get; set; }
    public virtual Cinema Cinema { get; set; }


}
