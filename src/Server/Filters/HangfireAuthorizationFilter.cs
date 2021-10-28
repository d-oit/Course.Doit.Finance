using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using Hangfire.Dashboard;

namespace BlazorHero.CleanArchitecture.Server.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return httpContext.User.IsInRole(Permissions.Hangfire.View);
        }
    }
}