using CodeEditor.Api.Exceptions;
using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Requests.AuthRequests;
using CodeEditor.Domain.Responses.AuthResponses;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.TokenSpecification;
using CodeEditor.Domain.Specifications.UserSpecification;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using LoginRequest = CodeEditor.Domain.Requests.AuthRequests.LoginRequest;

namespace CodeEditor.Domain.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserService _userService;
        private readonly IService<Token> _tokenService;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            IUserService userService,
            IService<Token> tokenService,
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var spec = new FindUserByUserNameSpecification(loginRequest.UserName);
            var user = await _userService.FindOneAsync(spec);

            if (!CheckPassword(loginRequest.Password, user!.Password))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized, "Login or password incorrect");
            }

            return new LoginResponse
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = await GenerateRefreshToken(user),
                UserId = user.Id
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

        public string GenerateAccessToken(User user)
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

            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationInMinutes);

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

        public async Task<string> GenerateRefreshToken(User user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpirationInMinutes);

            var token = new JwtSecurityToken(
                notBefore: DateTime.UtcNow,
                expires: expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var serializedToken = tokenHandler.WriteToken(token);

            var spec = new FindTokenByUserIdSpecification(user.Id);
            var tokenEntity = await _tokenService.FindOneAsync(spec);

            if (tokenEntity != null)
            {
                await _tokenService.DeleteAsync(tokenEntity);
            }

            _ = await _tokenService.AddAsync(new Token
            {
                RefreshToken = serializedToken,
                UserId = user.Id
            });

            return serializedToken;
        }

        private bool CheckPassword(string requestPassword, string dbPassword)
        {
            return BCrypt.Net.BCrypt.Verify(requestPassword, dbPassword);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 5);
        }

        public async Task<LoginResponse> RefreshToken(string refreshToken)
        {
            var expirationDate = GetExpirationDateFromJwtToken(refreshToken);
            if (expirationDate == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest, "Unable to find token expiration date");
            }

            if (expirationDate < DateTime.UtcNow)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized, "Token is expired");
            }

            var spec = new FindTokenByRefreshTokenSpecification(refreshToken);
            spec.AddInclude((entity) => entity.User);
            var token = await _tokenService.FindOneAsync(spec);

            if (token == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.Unauthorized, "Token or user does not exist");
            }

            return new LoginResponse
            {
                AccessToken = GenerateAccessToken(token.User),
                RefreshToken = await GenerateRefreshToken(token.User),
                UserId = token.UserId
            };

        }

        private DateTime? GetExpirationDateFromJwtToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(refreshToken);

            var expirationClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
            if (expirationClaim != null && long.TryParse(expirationClaim.Value, out long exp))
            {
                return DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
            }

            return null;
        }
    }
}
