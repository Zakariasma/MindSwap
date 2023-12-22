using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MindSwipe.Data;
using MindSwipe.Models;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MindSwipe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UsersController usersController;
        

        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, MindSwipeContext context) 
        {
            _configuration = configuration;
            usersController = new UsersController(context);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Users>> Register(string username,string password,string email)
        {
            if (usersController.GetUserByName(username).Result.Value != null)
            {
                return Problem("User already exists");
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            Users user = new Users();
            user.Username = username;
            user.Email = email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await usersController.PostUser(user);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            Users user = usersController.GetUserByName(username).Result.Value;

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if(!VerifyPasswordHash(user, password))
            {
                return BadRequest("Wrong password");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddHours(5),
                signingCredentials:creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using(var hmac = new HMACSHA512()) 
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(Users user,string password)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(user.PasswordHash);
            }
        }
    }
}
