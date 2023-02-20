using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
public class Startup
{
    public IConfiguration configRoot
    {
        get;
    }
    public Startup(IConfiguration configuration)
    {
        configRoot = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMicrosoftIdentityWebAppAuthentication(configRoot, "CRUDProject");
        services.AddMvc(option =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            option.Filters.Add(new AuthorizeFilter(policy));
        }).AddMicrosoftIdentityUI();
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseAuthentication();
    }
}