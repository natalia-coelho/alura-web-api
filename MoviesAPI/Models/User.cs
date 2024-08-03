using Microsoft.AspNetCore.Identity;

namespace MoviesAPI.Models
{
    public class User : IdentityUser
    {
        // identity will create the fields :) 
        // create an personalized User is that we can add aditional properties to the user 
        public DateTime BirthDate { get; set; }

        public User() : base() // base serve para chamar as propriedades do IdentityUser 
        {
        }
    }
}
