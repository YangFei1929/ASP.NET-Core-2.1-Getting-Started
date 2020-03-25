using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreProjDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /*
         从控制台进行参数配置, 我们进入到项目目录,  运行 dotnet run WelcomeMsg="AAA",  然后访问http://localhost:5000/,  输出的就是AAA, 所以可以说, 命令行参数优先级是最高的
         */
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            //WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((context,builder)=> { builder.AddJsonFile("path")})//可以自定义配置方式
             WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseKestrel(options=>//设置Kestrel服务器
                {
                    //以下配置让.NET Core CLI（dotnet run）运行项目，则使用带有"commandName": "Project",的配置文件 时，同时可以进行http和https的访问。
                    options.Listen(IPAddress.Loopback, 5000);
                    options.Listen(IPAddress.Loopback, 5001, listenOptions => listenOptions.UseHttps("E:\\ASP.NET CORE Study\\CoreProjDemo\\CoreProjDemo\\wwwroot\\coreIISexpress.pfx", "yf110120119"));
                    //options.UseSystemd();
                    }
                )
            ;
    }
}
