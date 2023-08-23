using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly customerDbContext _customerDbContext;
        
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, customerDbContext customerDbContext)
        {
            _configuration = configuration;
            _customerDbContext = customerDbContext;
            //user.Username = "wells";
            //user.Role = "Admin";
            //CreatePasswordHash("pass", out byte[] passwordHash2, out byte[] passwordSalt2);
            //user.PasswordSalt = passwordSalt2;  
            //user.PasswordHash= passwordHash2;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = new User();
         user.Username = request.Username;
            user.Role = request.Role;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.accNo=request.accNo;

            if (user.Role == "User")
            {
/*                var uname = _customerDbContext.customer.Find(user.accNo);
                var temp = _customerDbContext.User.Find(uname);*/
                if (_customerDbContext.customer.Find(user.accNo)==null /*|| temp==null*/)
                {
                    return BadRequest("Customer Does Not Exist");
                }

            }
            _customerDbContext.User.Add(user);
            _customerDbContext.SaveChanges();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (_customerDbContext.User.Find(request.Username)==null)
            {
                return BadRequest("User not found.");
            }
            User user = _customerDbContext.User.Find(request.Username);
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(user); 

            return Ok(new { token = token, role = user.Role, accNo=user.accNo});
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
