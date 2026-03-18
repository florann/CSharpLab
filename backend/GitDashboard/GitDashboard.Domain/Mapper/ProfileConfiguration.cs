using AutoMapper;
using CodeEditor.Domain.Entities;
using CodeEditor.Domain.Records;
using CodeEditor.Domain.Responses.GitRepoResponses;
using CodeEditor.Domain.Responses.UserResponses;
using GitDashboard.Domain.Responses.GitEntryResponses;

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
            
            CreateMap<GitRepoSummary, GitRepoTitleResponse>();
            CreateMap<GitRepoTitleResponse, GitRepoSummary>();

            CreateMap<GitFeedEntry, GitFeedEntryResponse>();
            CreateMap<GitFeedEntryResponse, GitFeedEntry>();
        }
    }
}
