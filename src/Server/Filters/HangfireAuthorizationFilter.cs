using Hangfire.Dashboard;
using System;

namespace BlazorHero.CleanArchitecture.Server.Filters
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                return true;
            }

            var httpContext = context.GetHttpContext();

            if (httpContext != null & httpContext.User.Identity.IsAuthenticated & httpContext.User.IsInRole("Permissions.Hangfire.View"))
            {
                return true;
            }

            return false;


        }
    }
}