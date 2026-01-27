using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Requests.AuthRequests;
using CodeEditor.Domain.Responses.AuthResponses;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<bool> CreateAccount(CreateAccountRequest loginRequest);
        string GenerateAccessToken(User user);
        Task<LoginResponse> RefreshToken(RefreshTokensRequest refreshRequest);
    }
}
