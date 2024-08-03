using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MoviesAPI.Data.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class UserService
    {
        private UserDbContext _userDbContext;
        private SignInManager<User> _signInManager;
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private TokenService _tokenService;

        public UserService(UserDbContext userDbContext, SignInManager<User> signInManager, IMapper mapper, UserManager<User> userManager, TokenService tokenService)
        {
            _userDbContext = userDbContext;
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
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

        public async Task RegisterUser(CreateUserDTO userDTO)
        {
            try
            {
                User user = _mapper.Map<User>(userDTO);
                //create user at the database
                IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<string> LoginAsync(LoginUserDTO userDto)
        {
            // isPersistent -> cookie vai persistir depois que o usuário logar: False
            var result = await _signInManager.PasswordSignInAsync(userDto.Username, userDto.Password, false, false);
            
            if (!result.Succeeded)
                throw new Exception("Login inválido");
            
            var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName ==  userDto.Username);
            
            if (user == null)
                throw new Exception("falha na autenticação"!);
            
            var token = _tokenService.GenerateToken(user);

            return token;
        }
    }
}
