using CodeEditor.Domain.Responses.UserResponses;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IUserService : IService<Entities.User>
    {
        Task<UserResponse> GetUserById(long userId);
    }
}
