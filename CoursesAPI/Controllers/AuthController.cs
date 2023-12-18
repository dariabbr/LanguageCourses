using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CoursesAPI.DTO;
using CoursesAPI.Entities;
using CoursesAPI.Interfaces;
using CoursesAPI.Models;

namespace CoursesAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
        
    public AuthController(IConfiguration configuration, IUserService userService, IMapper mapper)
    {
        _configuration = configuration;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        var user = _userService.GetAllUsers()
            .FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);
        if (user is null)
        {
            return Unauthorized();
        }
        var token = GenerateJwtToken(user);

        var returnModel = _mapper.Map<LoginReturnDto>(user);
        returnModel.Token = token;
        return Ok(returnModel);
    }

    private string GenerateJwtToken(User first)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] 
            {
                new Claim(ClaimTypes.Name, first.FirstName),
                new Claim(ClaimTypes.Role, first.Roles.Order().First().ToString())
            }),
            Expires = DateTime.Now.AddMinutes(key.ExpiryInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key.SecretKey)), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}