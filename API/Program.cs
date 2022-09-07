using System;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Creates Kestrel Server with some properties
               //CreateHostBuilder(args).Build().Run();
            //Custom Code
            var host=CreateHostBuilder(args).Build();
            using var scope=host.Services.CreateScope();
            var context= scope.ServiceProvider.GetRequiredService<APIContext>();
            var logger= scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try{
                context.Database.Migrate();
                DBIntializer.Intialize(context);
            }
            catch(Exception ex){
                logger.LogError(ex,"Problem intializing data");
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
