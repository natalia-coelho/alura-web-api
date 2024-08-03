using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Data.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class RegisterUserService
    {
        // foi preciso criar uma injecao de dependencia addscoped lá no program.cs, o Imapper e o UserManager já vem com essa configuraçã por padrão, por isso n precisou 

        private IMapper _mapper;
        private UserManager<User> _userManager;
        private UserDbContext _userDbContext;

        public RegisterUserService(UserManager<User> userManager, IMapper mapper, UserDbContext userDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userDbContext = userDbContext;
        }

        public async Task RegisterUser(CreateUserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                //create user at the database
                IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public List<User> GetUsers()
        {
            return _userDbContext.Users.ToList();
        }

        public User GetUserById(string id)
        {
            var user = _userDbContext.Users
        .FirstOrDefault(u => u.Id == id);
            return user;
        }
    }
}