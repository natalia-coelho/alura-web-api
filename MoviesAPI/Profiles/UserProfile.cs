using AutoMapper;
using MoviesAPI.Data.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<CreateUserDTO, User>();
        }
    }
}
