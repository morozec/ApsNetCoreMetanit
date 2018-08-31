using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HelloApp
{
    public class RoutingMiddleware
    {
        private RequestDelegate _next;
        public RoutingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //http://localhost:8979/home/index?token=12345
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value.ToLower();// /home/index
            if (path == "/" || path == "/index")
                await context.Response.WriteAsync("Home page");
            else if (path == "/about")
                await context.Response.WriteAsync("About");
            else
                context.Response.StatusCode = 404;
        }   
    }
}