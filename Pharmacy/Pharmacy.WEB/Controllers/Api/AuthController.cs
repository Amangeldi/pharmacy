using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pharmacy.BLL.DTO;
using Pharmacy.DAL.Entities;

namespace Pharmacy.WEB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IConfiguration _configuration;
        readonly UserManager<User> _userManager;
        public AuthController(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserDTO model)
        {
            var claims = await GetIdentity(model.Email, model.Password);
            if (claims == null)
            {
                LoginResultDTO resp = new LoginResultDTO
                {
                    Successful = false,
                    Error = "Username and password are invalid."
                };
                return Ok(resp);
            }
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: _configuration["ISSUER"],
                    audience: _configuration["AUDIENCE"],
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(Convert.ToInt32(_configuration["LIFETIME"]))),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["KEY"])), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            LoginResultDTO LoginResultDTO = new LoginResultDTO
            {
                Successful = true,
                Token = encodedJwt
            };
            return Ok(LoginResultDTO);
        }
        [HttpGet("/CheckLoginAdmin")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<AdministratorDTO>> CheckLoginAdmin(string id)
        {
            return Ok("JWT admin is true");
        }
        [HttpGet("/CheckLogin")]
        [Authorize]
        public async Task<ActionResult<AdministratorDTO>> CheckLogin(string id)
        {
            return Ok("JWT is true");
        }
        private async Task<IEnumerable<Claim>> GetIdentity(string username, string password)
        {
            User user = await _userManager.FindByNameAsync(username);
            var result = await _userManager.CheckPasswordAsync(user, password);
            if (!result)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                };
            return claims;
        }
    }
}