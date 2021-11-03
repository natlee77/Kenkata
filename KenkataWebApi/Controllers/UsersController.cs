using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KenkataWebApi.Data;
using KenkataWebApi.Entities;
using SharedLibrary.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace KenkataWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly KenkataDbContext _context;
        public IConfiguration _configuration { get; }


        public UsersController(KenkataDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var user = new User {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Address = model.Address,
                    City = model.City,
                    ZipCode = model.ZipCode,
                    Phonenumber = model.PhoneNumber
                };
                user.GeneratePassword(model.Password);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new OkResult();
            }
            catch { }
            return new BadRequestResult();
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if(user != null)
                {
                    if(user.ValidatePassword(model.Password))
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var expiresDate = DateTime.Now.AddMinutes(30);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim("UserId", user.Id.ToString()),
                                new Claim("Expires", expiresDate.ToString())
                            }),
                            Expires = expiresDate,
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value)), SecurityAlgorithms.HmacSha512Signature)
                        };

                        var _accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

                        return new OkObjectResult(new 
                        { 
                            AccessToken = _accessToken, 
                            Id = user.Id,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.Phonenumber,
                            Address = user.Address,
                            City = user.City,
                            ZipCode = user.ZipCode
                        });
                    }
                }
            }
            catch { }

            return new BadRequestResult();
        }

       
        
    }
}
