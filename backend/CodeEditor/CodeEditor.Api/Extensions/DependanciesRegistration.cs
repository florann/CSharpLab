namespace CodeEditor.Api.Extensions
{
    public static class DependanciesRegistration
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddServices()
            {
                services.AddSignalR(options =>
                {
                    options.EnableDetailedErrors = true;
                });

                return services;
            }
        }
    }
}
