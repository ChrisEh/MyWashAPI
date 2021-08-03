using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationPlugin;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyWashApi.Data.Models;
using MyWashApi.Dtos;

namespace MyWashApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        private MyWashContext _ctx;
        private IHttpContextAccessor _httpContextAccessor;
        private IMapper _mapper;

        public AccountsController(MyWashContext ctx, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, 
            IMapper mapper)
        {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            _ctx = ctx;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserCreateDto user)
        {
            if (_ctx.Users.SingleOrDefault(u => u.Email == user.Email) != null) 
                return BadRequest("User with this email already exists.");

            var newUser = _mapper.Map<UserCreateDto, User>(user);
            newUser.Role = "User";
            newUser.Password = SecurePasswordHasherHelper.Hash(user.Password);

            _ctx.Users.Add(newUser);

            await _ctx.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(string userId)
        {
            var user = _ctx.Users.FirstOrDefault(u => u.Id.ToString() == userId);
            if (user == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserDetails(UserUpdateDto userUpdateDto)
        {
            var userToUpdate = _mapper.Map<UserUpdateDto, User>(userUpdateDto);
            if (userToUpdate == null) return StatusCode(StatusCodes.Status404NotFound);
            _ctx.Update(userToUpdate);
            await _ctx.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserLoginDto userLoginData)
        {
            var user = _mapper.Map<UserLoginDto, User>(userLoginData);
            if (user == null) return StatusCode(StatusCodes.Status404NotFound);
            var hashedPassword = user.Password;
            if (!SecurePasswordHasherHelper.Verify(user.Password, hashedPassword)) return Unauthorized();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                token_type = token.TokenType,
                user_Id = user.Id,
                user_name = user.Name,
                expires_in = token.ExpiresIn,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
            });
        }
    }
}
