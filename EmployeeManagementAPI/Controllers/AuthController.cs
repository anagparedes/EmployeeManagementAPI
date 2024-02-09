using AutoMapper;
using EmployeeManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Employee employee = new Employee();
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;

        }
        [HttpPost("register")]
        public ActionResult<Employee> Register(UserDto request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            employee.UserName = request.Username;
            employee.PasswordHash = passwordHash;

            return Ok(_mapper.Map<Employee>(employee));
        }

        [HttpPost("login")]
        public ActionResult<Employee> Login(UserDto request)
        {
            if (employee.UserName != request.Username)
            {
                return BadRequest("User not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, employee.PasswordHash))
            {
                return BadRequest("Wrong Password");
            }

            string token = CreateToken(employee);

            return Ok(token);
        }

        private string CreateToken(Employee user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}
