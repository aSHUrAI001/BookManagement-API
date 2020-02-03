using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookApiWithCore.Domain;
using BookApiWithCore.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BookApiWithCore.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        public IdentityService(UserManager<IdentityUser> identityservice, JwtSettings jwtSettings)
        {
            _userManager = identityservice;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user= await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exists" }
                };
            }
            var userHasValidpassword = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidpassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/Password combination didn't match" }
                };
            }
            return GenerateAuthenticationResultForUser(user);
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var userExists = await _userManager.FindByEmailAsync(email);
            if (userExists != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email is already exists" }
                };
            }
            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email,
            };
            var createdUser = await _userManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)

                };
            }
            return GenerateAuthenticationResultForUser(newUser);

        }

        private AuthenticationResult GenerateAuthenticationResultForUser(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims: new[]
                {
                    new Claim(type:JwtRegisteredClaimNames.Sub,value:user.Email),
                    new Claim(type:JwtRegisteredClaimNames.Jti,value:Guid.NewGuid().ToString()),
                    new Claim(type:JwtRegisteredClaimNames.Email,value:user.Email),
                    new Claim(type:"id",value:user.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
