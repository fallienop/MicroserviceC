// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using FreeCourse.IdentityServer.Data;
using FreeCourse.IdentityServer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;

namespace FreeCourse.IdentityServer
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                // uncomment to write to Azure diagnostics stream
                //.WriteTo.File(
                //    @"D:\home\LogFiles\Application\identityserver.txt",
                //    fileSizeLimitBytes: 1_000_000,
                //    rollOnFileSizeLimit: true,
                //    shared: true,
                //    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            try
            {
                //var seed = args.Contains("/seed");
                //if (seed)
                //{
                //    args = args.Except(new[] { "/seed" }).ToArray();
                //}

                var host = CreateHostBuilder(args).Build();

                //if (seed)
                //{
                //    Log.Information("Seeding database...");
                //    var config = host.Services.GetRequiredService<IConfiguration>();
                //    var connectionString = config.GetConnectionString("DefaultConnection");
                //    SeedData.EnsureSeedData(connectionString);
                //    Log.Information("Done seeding database.");
                //    return 0;
                //}

                using(var scope=host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var dbcontext = services.GetRequiredService<ApplicationDbContext>();
                    dbcontext.Database.Migrate();

                    var usermanager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    if (!usermanager.Users.Any())
                    {
                        usermanager.CreateAsync(new ApplicationUser { UserName = "fallien" ,Email="shahin.babayev.200@gmail.com",City="cosmos"},"1Parola7").Wait();
                    }
                }

                Log.Information("Starting host...");
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
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