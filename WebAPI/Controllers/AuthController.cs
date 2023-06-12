using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Entities.Models;
using WebAPI.Repository.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly Inewrepository _newrepository;

        public AuthController(IConfiguration configuration, Inewrepository newrepository)
        {
            _configuration = configuration;
            _newrepository = newrepository;
        }

        [HttpPost("Register")]
        public ActionResult<User> Register(UserDto request)
        {
            string passwordhash = BCrypt.Net.BCrypt.HashPassword(request.Password); // make password hash coded
            user.Passwordhash = passwordhash;
            user.Username = request.Username;
            return Ok(user);
        }
        [HttpPost("Login")]
        public ActionResult<User> Login(UserDto request)
        {
            if (user.Username != request.Username)
            {
                return BadRequest("User Not Found!");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Passwordhash))
            {
                return BadRequest("Wrong Password!");
            }

            var token = CreateToken(user);
            return Ok(token);
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "ADMIN")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
