using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MariaDB.Extensions;
using System;
using System.Diagnostics;
using System.IO;

namespace WebAPI
{
    public class Program
    {
        public static IConfiguration Configuration { get; } =
          new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
              optional: false,
              reloadOnChange: true)
          .Build();

        public static void Main(string[] args)
        {
            string connectionString = Configuration["ConnectionStrings:DbConnection"];

            Log.Logger = new LoggerConfiguration()
            //.ReadFrom.Configuration(Configuration, sectionName: "Serilog")
            .WriteTo.MariaDB(
                connectionString,
                restrictedToMinimumLevel: LogEventLevel.Error,
                autoCreateTable:true)
            .Enrich.FromLogContext()
            .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg =>
            {
                Debug.Print(msg);
            });

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
