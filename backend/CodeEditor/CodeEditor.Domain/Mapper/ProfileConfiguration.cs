using AutoMapper;
using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Responses.GitRepoResponses;
using CodeEditor.Domain.Responses.UserResponses;

namespace CodeEditor.Domain.Mapper
{
    public class ProfileConfiguration : Profile
    {
        public ProfileConfiguration()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserResponse, User>();

            CreateMap<GitRepo, GitRepoResponse>();
            CreateMap<GitRepoResponse, GitRepo>();
        }
    }
}
