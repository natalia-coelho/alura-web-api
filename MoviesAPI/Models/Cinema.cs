using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models;
public class Cinema
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = $"Field's Name is mandatory.")]
    public string Name { get; set; }
    public int AddressId { get; set; }
    public virtual Address Address{ get; set; }
}