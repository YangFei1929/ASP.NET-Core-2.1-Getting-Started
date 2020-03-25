using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreProjDemo.Services;
using CoreProjDemo.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreProjDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IWelcome,Welcome>();
            services.AddSingleton<ITestService, TestService>();
            //依赖注入
            services.Configure<ConnectionOptions>(_configuration.GetSection("ConnectionStrings"));//这里从appsettings.json里将ConnectionStrings区域信息注入到了ConnectionOptions类中
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IConfiguration configuration,IWelcome welcome)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //var builder = new ConfigurationBuilder().AddJsonFile(path);
            //var configuration = builder.Build();
            /*app.Run(async (context) =>
            {
                //await context.Response.WriteAsync("Hello World!");
                var msg = configuration["WelcomeMsg"];
                await context.Response.WriteAsync(msg);
                //await context.Response.WriteAsync("Hello World!");
            });*/
            //要将程序发布到linux系统下需配置反向代理的内容
            //if(env.IsProduction())一般在生产环境下采用下面的UseForwardedHeaders策略
            //{

            //}
            app.UseForwardedHeaders(new ForwardedHeadersOptions {
                ForwardedHeaders=ForwardedHeaders.XForwardedFor|ForwardedHeaders.XForwardedProto
            });

            app.Use(next =>
            {
                return async context =>
                {
                    if (context.Request.Path.StartsWithSegments("/first"))
                    {
                        await context.Response.WriteAsync("First");
                    }
                    else
                    {
                        await next(context);
                    }
                };
            });
             app.UseWelcomePage(new WelcomePageOptions
             {
                 Path = "/welcome"
             });

            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseMvc(routes=> 
            {
                routes.MapRoute(name:"default",template:"{controller=Home}/{action=Index}/{id?}");
            });


            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/plain;charset=utf-8";
                var msg = welcome.GetWelcomeMsg();
                await context.Response.WriteAsync(msg);
            });
            //app.UseWelcomePage();
        }
    }
}
