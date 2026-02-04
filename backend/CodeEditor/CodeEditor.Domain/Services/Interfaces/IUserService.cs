using CodeEditor.Domain.Responses.UserResponses;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetUserById(long userId);
    }
}
