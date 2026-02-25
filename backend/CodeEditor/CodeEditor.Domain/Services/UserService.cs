using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.UserSpecification;

namespace CodeEditor.Domain.Services
{
    public class UserService(
        IRepository<Entities.User> repository) : Service<Entities.User>(repository), IUserService
    {
        public async Task<User?> GetUserById(long userId)
        {
            var spec = new FindUserByIdSpecification(userId);
            var user = await _repository.FindOneAsync(spec);
            return user;
        }
    }
}
