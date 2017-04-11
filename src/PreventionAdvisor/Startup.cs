using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.Entity.Extensions;
using System;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PreventionAdvisor;
using PreventionAdvisor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using AutoMapper;
using PreventionAdvisor.ViewModels;

public class Startup
{
    public IConfigurationRoot Configuration { get; }

    private readonly IHostingEnvironment env;

    public Startup(IHostingEnvironment env)
    {
        this.env = env;

        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddJsonFile("vcap-local.json", optional:true); // when running locally, store VCAP_SERVICES credentials in vcap-local.json

        Configuration = builder.Build();

        string vcapServices = Environment.GetEnvironmentVariable("VCAP_SERVICES");
        if (vcapServices != null)
        {
            dynamic json = JsonConvert.DeserializeObject(vcapServices);
            if (json.cleardb != null)
            {
                try
                {
                    Configuration["cleardb:0:credentials:uri"] = json.cleardb[0].credentials.uri;
                }
                catch (Exception)
                {
                    // Failed to read ClearDB uri, ignore this and continue without a database
                }
            }
        }
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var databaseUri = Configuration["cleardb:0:credentials:uri"];
        if (!string.IsNullOrEmpty(databaseUri))
        {
            // add database context
            services.AddDbContext<PreventionAdvisorDbContext>(options => options.UseMySQL(getConnectionString(databaseUri)));
        }

        // Add CORS
        services.AddCors();

        // Add framework services.
        services.AddMvc();

        // Always use HTTPS

        if (!env.IsDevelopment())
            services.Configure<MvcOptions>(o => o.Filters.Add(new RequireHttpsAttribute()));

        // Add Identity
        services.AddIdentity<User, IdentityRole>(config =>
        {
            config.User.RequireUniqueEmail = true;
            config.Password.RequiredLength = 8;
            config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
            config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
            {
                OnRedirectToLogin = async ctx =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                    {
                        ctx.Response.StatusCode = 401;
                    }
                    else
                    {
                        ctx.Response.Redirect(ctx.RedirectUri);
                    }

                    await Task.Yield();
                }
            };

        })
        .AddEntityFrameworkStores<PreventionAdvisorDbContext>();

        // Add automapper
        MapperConfig.CreateMappings();
    }

    private string getConnectionString(string databaseUri)
    {
        var connectionString = "";
        try
        {
            string hostname;
            string username;
            string password;
            string port;
            string database;
            username = databaseUri.Split('/')[2].Split(':')[0];
            password = (databaseUri.Split(':')[2]).Split('@')[0];
            var portSplit = databaseUri.Split(':');
            port = portSplit.Length == 4 ? (portSplit[3]).Split('/')[0] : null;
            var hostSplit = databaseUri.Split('@')[1];
            hostname = port == null ? hostSplit.Split('/')[0] : hostSplit.Split(':')[0];
            var databaseSplit = databaseUri.Split('/');
            database = databaseSplit.Length == 4 ? databaseSplit[3] : null;
            var optionsSplit = database.Split('?');
            database = optionsSplit.First();
            port = port ?? "3306"; // if port is null, use 3306
            connectionString = $"Server={hostname};uid={username};pwd={password};Port={port};Database={database};SSL Mode=Required;";
        }
        catch (IndexOutOfRangeException ex)
        {
            throw new FormatException("Invalid database uri format", ex);
        }

        return connectionString;
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, UserManager<User> userManager)
    {
        loggerFactory.AddConsole(Configuration.GetSection("Logging"));
        loggerFactory.AddDebug();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            //app.UseHsts(h => h.MaxAge(days: 1)); // if hsts is set: there is no way back...
            app.UseCsp(options => options.DefaultSources(s => s.Self())); // only allow content from same-origin
            app.UseXfo(o => o.Deny()); // Anti click-jack
        }

        var context = (app.ApplicationServices.GetService(typeof(PreventionAdvisorDbContext)) as PreventionAdvisorDbContext);
//        context?.Database.Migrate();

        app.UseStaticFiles();

        // Use identity
        app.UseIdentity();

        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");

                // when the user types in a link handled by client side routing to the address bar 
                // or refreshes the page, that triggers the server routing. The server should pass 
                // that onto the client, so Angular can handle the route
                routes.MapRoute(
                    name: "spa-fallback",
                    template: "{*url}",
                    defaults: new { controller = "Home", action = "Index" }
                );
        });

//#if DEBUG
        app.UseCors(builder => {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
//#endif


        DbInitializer.Initialize(context, userManager);
    }
}
