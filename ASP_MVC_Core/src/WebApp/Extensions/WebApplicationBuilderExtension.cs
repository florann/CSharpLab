namespace WebApp.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        extension(WebApplicationBuilder webApplicationBuilder) { 
            public WebApplicationBuilder ConfigureDatabaseContext()
            {
                webApplicationBuilder.Configuration.

                return webApplicationBuilder;
            }
        }
    }
}
