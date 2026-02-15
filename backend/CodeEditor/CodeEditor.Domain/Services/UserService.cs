using AutoMapper;
using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Responses.UserResponses;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications.UserSpecification;

namespace CodeEditor.Domain.Services
{
    public class UserService(
        IRepository<Entities.User> repository, 
        IMapper mapper) : Service<Entities.User>(repository, mapper), IUserService
    {
        public async Task<UserResponse> GetUserById(long userId)
        {
            var spec = new FindUserByIdSpecification(userId);
            var user = await _repository.FindOneAsync(spec);
            return _mapper.Map<UserResponse>(user);
        }
    }
}
