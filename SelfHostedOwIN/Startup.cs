using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedOwinWeb
{
    internal class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            //Add custom middleware
            appBuilder.Use(async (context, next) =>
            {
                //log request
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] - {context.Request.Uri} - {context.Request.RemoteIpAddress} - {context.Request.User}");

                context.Response.Headers["custom"] = "SelfHostedOwinWeb";

                //Call next middleware
                await next.Invoke();
            });

            var options = new FileServerOptions
            {
                EnableDirectoryBrowsing = true,
                EnableDefaultFiles = true,
                DefaultFilesOptions = { DefaultFileNames = { "index.html" } },               
                FileSystem = new PhysicalFileSystem(".")                
            };
            options.StaticFileOptions.ContentTypeProvider = new CustomContentTypeProvider();
            appBuilder.UseFileServer(options);

            
        }
    }
}
