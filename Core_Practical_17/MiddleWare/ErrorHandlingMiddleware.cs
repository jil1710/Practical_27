using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Core_Practical_17.MiddleWare
{
    
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await _next(httpContext);

            if(httpContext.Response.StatusCode==404 && !httpContext.Response.HasStarted && !httpContext.Request.Path.StartsWithSegments("/Error"))
            {
                httpContext.Response.Redirect("/Error/PageNotFound");
            }
            if(httpContext.Response.StatusCode==403 && !httpContext.Response.HasStarted && !httpContext.Request.Path.StartsWithSegments("/Error"))
            {
                httpContext.Response.Redirect("/Error/AccessDenied");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
