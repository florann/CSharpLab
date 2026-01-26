using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Requests.AuthRequests;
using CodeEditor.Domain.Responses.AuthResponses;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications;
using CodeEditor.Domain.Specifications.UserSpecification;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeEditor.Domain.Services
{
    public class AuthService : IAuthService
    {

        private readonly IBaseEntityService<Entities.User> _userService;
        private readonly JwtSettings _jwtSettings;

        public AuthService( 
            IBaseEntityService<Entities.User> userService,
            IOptions<JwtSettings> jwtSettings) 
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var spec = new FindUserByUserNameSpecification(loginRequest.UserName);
            var user = await _userService.GetAsync(spec);

            if(!CheckPassword(loginRequest.Password, user!.Password))
            {
                return new LoginResponse
                {
                    AccessToken = "",
                    RefreshToken = ""
                };
            }

            var token = GenerateToken(user);

            return new LoginResponse
            {
                AccessToken = token,
                RefreshToken = ""
            };
        }

        public async Task<bool> CreateAccount(CreateAccountRequest loginRequest)
        {
            User user = new()
            {
                UserName = loginRequest.UserName,
                Password = HashPassword(loginRequest.Password),
                Guid = Guid.NewGuid()
            };

            var result = await _userService.AddAsync(user);
            return result != null;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiration,
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private bool CheckPassword(string requestPassword, string dbPassword)
        {
            return BCrypt.Net.BCrypt.Verify(requestPassword, dbPassword);
        }
    
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 5);
        }
    }
}
