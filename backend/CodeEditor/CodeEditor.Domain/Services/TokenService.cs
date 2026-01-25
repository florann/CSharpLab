using CodeEditor.Domain.Repositories.Base;
using CodeEditor.Domain.Requests.AuthRequests;
using CodeEditor.Domain.Responses.AuthResponses;
using CodeEditor.Domain.Services.Interfaces;
using CodeEditor.Domain.Specifications;
using CodeEditor.Domain.Specifications.UserSpecification;

namespace CodeEditor.Domain.Services
{
    public class TokenService : BaseEntityService<Entities.Token>
    {

        private readonly IBaseEntityService<Entities.User> _userService;

        public TokenService(IRepository<Entities.Token> repository, 
            IBaseEntityService<Entities.User> userService) : base(repository)
        {
            _userService = userService;
        }

        public async Task<LoginResponse> GenerateTokens(LoginRequest loginRequest)
        {
            var spec = new FindUserByUserNameSpecification(loginRequest.UserName);
            var user = await _userService.GetAsync(spec);

            return new LoginResponse
            {
                AccessToken = "",
                RefreshToken = ""
            };
        }

        private bool CheckPassword(string requestPassword, string dbPassword)
        {

            return false;
        }
    }
}
