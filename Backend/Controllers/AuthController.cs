using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MovieSugges.MovieSugges.BL.Model;


//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly JwtTokenGenerator _jwtTokenGenerator;

//        private readonly List<User> _users = new List<User>
//        {
//            new User { Email = "admin", Password = "admin123", Role = "Admin" },
//            new User { Email = "user", Password = "user123", Role = "User" }
//        };

//        public AuthController(IOptions<JwtSettings> jwtSettings)
//        {
//            _jwtTokenGenerator = new JwtTokenGenerator(jwtSettings.Value);
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginDTO model)
//        {
//            var user = _users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);

//            if (user == null)
//            {
//                return Unauthorized();
//            }

//            var token = _jwtTokenGenerator.GenerateToken(user);
//            return Ok(new { Token = token });
//        }
//    }


    //public class JwtTokenGenerator
    //{
    //    private readonly JwtSettings _jwtSettings;

        //public JwtTokenGenerator(JwtSettings jwtSettings)
        //{
        //    _jwtSettings = jwtSettings;
        //}

    //    public string GenerateToken(User user)
    //    {
    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
    //        var tokenDescriptor = new SecurityTokenDescriptor
    //        {
    //            Subject = new ClaimsIdentity(new[]
    //            {
    //                new Claim(ClaimTypes.Name, user.FirstName),
    //                new Claim(ClaimTypes.Role, user.Role)
    //            }),
    //            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
    //            Issuer = _jwtSettings.Issuer,
    //            Audience = _jwtSettings.Audience,
    //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //        };

    //        var token = tokenHandler.CreateToken(tokenDescriptor);
    //        return tokenHandler.WriteToken(token);
    //    }
    //}
