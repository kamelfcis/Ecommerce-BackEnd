using AuthenticationPlugin;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.ComponentModel.DataAnnotations;
using BackEnd.DTO.Auth;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        private BotigaContext _dbContext;
        public AuthController(BotigaContext dbContext, IConfiguration configuration)
        {
            _configuration = configuration;
            _auth = new AuthService(_configuration);
            _dbContext = dbContext;
        }



        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Register account)
        {
            var accountWithSameEmail = _dbContext.Users.SingleOrDefault(u => u.Email == account.Email || u.Username == account.Username);
            if (accountWithSameEmail != null) return BadRequest("User with this  UserName already exists");
            var accountObj = new User
            {
                Username = account.Username,
                Phone = account.Phone,
                Email = account.Email,
                Password = SecurePasswordHasherHelper.Hash(account.Password),
                FullName = account.FullName,
                RoleId = account.RoleId,
            };
            _dbContext.Users.Add(accountObj);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

       
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(Login account)
        {
            var AccountUser = _dbContext.Users.FirstOrDefault(u => u.Username == account.Username);
            if (AccountUser == null) return StatusCode(StatusCodes.Status404NotFound);
            var hashedPassword = AccountUser.Password;
            if (!SecurePasswordHasherHelper.Verify(account.Password, hashedPassword)) return Unauthorized();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, AccountUser.Email ),
                new Claim(ClaimTypes.Name, AccountUser.Username),
                new Claim(ClaimTypes.Role,_dbContext.Roles.FirstOrDefault(x=>x.Id==AccountUser.RoleId).Name)
            };

            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                token_type = token.TokenType,
                user_Id = AccountUser.Id,
                user_name = AccountUser.Username,
                expires_in = token.ExpiresIn,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                accountid = AccountUser.Id,
                email = AccountUser.Email,
                role= _dbContext.Roles.FirstOrDefault(x => x.Id == AccountUser.RoleId).Name

            });
        }
        


    }
}
