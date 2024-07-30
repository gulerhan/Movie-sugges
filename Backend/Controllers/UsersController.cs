using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using MovieSugges.MovieSugges.BL.Model;

namespace MovieSugges.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        //private readonly JwtTokenGenerator _jwtTokenGenerator;

        private readonly List<User> _users = new List<User>
        {
            new User { Email = "admin", Password = "admin123", Role = "Admin" },
            new User { Email = "user", Password = "user123", Role = "User" }
        };

        public UsersController(MyDbContext dbContext, IOptions<JwtSettings> jwtSettings)
        {
            this.dbContext = dbContext;

            //_jwtTokenGenerator = new JwtTokenGenerator(jwtSettings.Value);
        }

        [HttpPost]
        [Route("Registration")]

        public IActionResult Registration(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var objUser = dbContext.Users.FirstOrDefault(x => x.Email == userDTO.Email);
            if (objUser == null)
            {

                dbContext.Users.Add(new User
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                });
                dbContext.SaveChanges();
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("User already exists with the same email address.");
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password);
            
            //var token = _jwtTokenGenerator.GenerateToken(user);

            //user.BearerToken = token;

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound("Kullanıcı bulunamadı.");

        }
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(dbContext.Users.ToList());
        }

        [HttpGet]
        [Route("GetUser")]
        [Authorize(Roles = "admin")]
        public IActionResult GetUser(int id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null)
                return Ok();
            else
                return NoContent();
        }
    }

}
