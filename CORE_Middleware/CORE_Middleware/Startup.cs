using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CORE_Middleware
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) => // KEndi MiddleWare' ımız.
            {
                if (context.Request.Path == "/profile")
                {
                    await context.Response.WriteAsync("Kullanıcının bilgileri");
                }
                else
                {
                    await next();
                }
            });

            app.Map("/orders",
                config =>
                config.Use(async (context, next) =>
                await context.Response.WriteAsync("Kullanıcının siparişleri")
                ));

            app.MapWhen(
                context => context.Request.Method == "POST" &&
                           context.Request.Path == "/account",
                config =>
                           config.Use(async (context, next) =>
                                            await context.Response.WriteAsync("Accounta Post işlemi yapıldığında çalışacaktır.")



                ));


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
