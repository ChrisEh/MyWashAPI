using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationPlugin;
using FoodApi.Data;
using FoodApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MyWashApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        private FoodDbContext _dbContext;
        private IHttpContextAccessor _httpContextAccessor;

        public AccountsController(FoodDbContext dbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(User user)
        {
            var userWithSameEmail = _dbContext.Users.SingleOrDefault(u => u.Email == user.Email);
            if (userWithSameEmail != null) return BadRequest("User with this email already exists");
            var userObj = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = SecurePasswordHasherHelper.Hash(user.Password),
                PhoneNumber = user.PhoneNumber,
                StreetName = user.StreetName,
                Place = user.Place,
                PostCode = user.PostCode,
                Role = "User"
            };
            _dbContext.Users.Add(userObj);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(string userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId);
            if (user == null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetails update)
        {
            var userToUpdate = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == update.Id);
            if (userToUpdate.Id.ToString() != update.Id) return StatusCode(StatusCodes.Status404NotFound);
            if (!string.IsNullOrWhiteSpace(update.StreetName)) { userToUpdate.StreetName = update.StreetName; }
            if (!string.IsNullOrWhiteSpace(update.HouseNumber)) { userToUpdate.HouseNumber = update.HouseNumber; }
            if (!string.IsNullOrWhiteSpace(update.PostCode)) { userToUpdate.PostCode = update.PostCode; }
            if (!string.IsNullOrWhiteSpace(update.Place)) { userToUpdate.Place = update.Place; }
            if (!string.IsNullOrWhiteSpace(update.PhoneNumber)) { userToUpdate.PhoneNumber = update.PhoneNumber; }
            _dbContext.Update(userToUpdate);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {
            var userEmail = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (userEmail == null) return StatusCode(StatusCodes.Status404NotFound);
            var hashedPassword = userEmail.Password;
            if (!SecurePasswordHasherHelper.Verify(user.Password, hashedPassword)) return Unauthorized();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, userEmail.Role)
            };

            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                token_type = token.TokenType,
                user_Id = userEmail.Id,
                user_name = userEmail.Name,
                expires_in = token.ExpiresIn,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
            });
        }
    }
}
