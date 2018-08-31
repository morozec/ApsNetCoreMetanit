using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HelloApp.Services
{
    public class MessageMiddleware
    {

        public MessageMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context, MessageService service)
        {
            await context.Response.WriteAsync(service.SendMessage());
        }
    }
}
