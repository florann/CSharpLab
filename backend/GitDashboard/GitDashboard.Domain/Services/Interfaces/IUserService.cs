using CodeEditor.Domain.Entities;

namespace CodeEditor.Domain.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        Task<User?> GetUserById(long userId);
    }
}
