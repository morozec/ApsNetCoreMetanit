using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HelloApp.Services.Counter
{
    public class CounterMiddleware
    {
        private int _i = 0;
        public CounterMiddleware(RequestDelegate next)
        {
            
        }

        public async Task InvokeAsync(HttpContext context, ICounter counter, CounterService counterService)
        {
            ++_i;
            await context.Response.WriteAsync(
                $"Request: {_i} ICounter: {counter.Value} CounterService: {counterService.Counter.Value}");
        }
    }
}