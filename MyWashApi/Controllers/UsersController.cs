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
using MyWashApi.Data;
using MyWashApi.Data.Models;
using MyWashApi.Dtos;
using MyWashApi.Service.Services;

namespace MyWashApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, 
            IMapper mapper, IUserService userService)
        {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserCreateDto user)
        {
            if (await _userService.UserExists(user.Email)) 
                return BadRequest("User with this email already exists.");

            var newUser = _mapper.Map<User>(user);
            await _userService.Register(newUser);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAsync(string userId)
        {
            var user = await _userService.GetUser(new Guid(userId));
            if (user == null) return StatusCode(StatusCodes.Status404NotFound);

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserDetails(UserUpdateDto userUpdateDto)
        {
            var userToUpdate = await _userService.GetUser(userUpdateDto.Id);
            userToUpdate.Name = userToUpdate.Name;
            userToUpdate.HouseNumber = userToUpdate.HouseNumber;
            userToUpdate.Orders = userToUpdate.Orders;
            userToUpdate.Place = userToUpdate.Place;
            userToUpdate.PostCode = userUpdateDto.PostCode;
            userToUpdate.PhoneNumber = userUpdateDto.PhoneNumber;

            if (userToUpdate == null) return StatusCode(StatusCodes.Status404NotFound);
            await _userService.Update(userToUpdate);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginData)
        {
            var user = await _userService.GetUser(userLoginData.Email);

            if (user == null) return StatusCode(StatusCodes.Status404NotFound);

            var hashedPassword = user.Password;
            if (!SecurePasswordHasherHelper.Verify(userLoginData.Password, hashedPassword)) return Unauthorized();
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
