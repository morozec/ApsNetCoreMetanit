using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloApp.Services;
using HelloApp.Services.Counter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HelloApp
{
    public class Startup
    {
        private string _name;
        public Startup()
        {
            _name = "Tom";
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICounter, RandomCounter>();//AddTransient, AddSingleton
            services.AddTransient<CounterService>();

            services.AddMvc();
            services.AddTransient<IMessageSender, SmsMessageSender>();
            services.AddTransient<TimeService>();
            services.AddTransient<MessageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IMessageSender sender, TimeService timeService, MessageService service)
        {
            app.UseMiddleware<CounterMiddleware>();

            app.UseMiddleware<MessageMiddleware>();

            //app.Run(async context => { await context.Response.WriteAsync(sender.Send() + timeService.GetTime()); });
            app.Run(async context =>
            {
                var otherWayService = context.RequestServices.GetService<MessageService>();//или GetRequiredService
                await context.Response.WriteAsync(otherWayService.SendMessage());
                //await context.Response.WriteAsync(service.SendMessage());
            });


            DefaultFilesOptions opt = new DefaultFilesOptions();
            opt.DefaultFileNames.Clear();
            opt.DefaultFileNames.Add("hello.html");

            app.UseDefaultFiles(opt); //default.html index.html
            app.UseStaticFiles();  //hello.html

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMiddleware<RoutingMiddleware>();

            //app.UseMiddleware<TokenMiddleware>();
            app.UseToken("555");

            app.Map("/home",
                home =>
                {
                    home.Map("/index", // home/index
                        (appBuilder) =>
                        {
                            appBuilder.Run(async (context) =>
                            {
                                context.Response.ContentType = "text/html;charset=utf-8";
                                await context.Response.WriteAsync("<h2>Home page</h2>");
                            });
                        });

                    home.Map("/about", About); // home/about
                });

            app.Map("/index",
                (appBuilder) =>
                {
                    appBuilder.Run(async (context) =>
                    {
                        context.Response.ContentType = "text/html;charset=utf-8";
                        await context.Response.WriteAsync("<h2>Home page</h2>");
                    });
                });

            app.Map("/about",About);

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}


            int x = 5;
            int y = 2;
            int z = 0;
            app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync("Hello!");
                z = x * y; //z = 10
                await next();

                z = z * 5; // z = 100
                await context.Response.WriteAsync($"z = {z}");
            });

            //app.Use()
            //app.Map
            //app.MapWhen
            //app.UseWhen
            //app.Run
            //app.UseMiddleware
            app.Run(async context =>
            {
                //await context.Response.WriteAsync("Hello world!");

                //await context.Response.WriteAsync($"z = {z}");
                z *= 2; //z = 20
                await Task.FromResult(0);
            } );
        }

        private void About(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html;charset=utf-8";
                await context.Response.WriteAsync("<h2>About</h2>");
            });
        }

        private async Task Handle(HttpContext context)
        {
            var host = context.Request.Host.Value;
            var path = context.Request.Path;
            var query = context.Request.QueryString.Value;

            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync($"<h3>Хост: {host}</h3>" +
                                              $"<h3>Путь запроса: {path}</h3>" +
                                              $"<h3>Параметры строки запроса: {query}</h3>");
        }
    }
}
