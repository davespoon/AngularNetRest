using System;
using DimkasBoardGames.Models;
using DimkasBoardGames.Repositories;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DimkasBoardGames
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      //Add PostgreSQL support
      //services.AddDbContext<CustomersDbContext>(options => {
      //    options.UseNpgsql(Configuration.GetConnectionString("CustomersPostgresConnectionString"));
      //});

      //Add SQL Server support
      //services.AddDbContext<CustomersDbContext>(options => {
      //    options.UseSqlServer(Configuration.GetConnectionString("CustomersSqlServerConnectionString"));
      //});

      //Add SqLite support
      services.AddDbContext<AppDbContext>(options =>
      {
        options.UseSqlite(Configuration.GetConnectionString("CustomersSqliteConnectionString"));
      });

      services.AddControllers();

      services.AddSpaStaticFiles(configuration => { configuration.RootPath = "Client/dist"; });

      // Handle XSRF Name for Header
      services.AddAntiforgery(options => { options.HeaderName = "X-XSRF-TOKEN"; });

      //https://github.com/domaindrivendev/Swashbuckle.AspNetCore
      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "Application API",
          Description = "Application Documentation",
          Contact = new OpenApiContact {Name = "Author"},
          License = new OpenApiLicense {Name = "MIT", Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")}
        });
      });

      services.AddCors(o => o.AddPolicy("AllowAllPolicy", options =>
      {
        options.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
      }));

      services.AddCors(o => o.AddPolicy("AllowSpecific", options =>
        options.WithOrigins("http://localhost:4200")
          .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
          .WithHeaders("accept", "content-type", "origin", "X-Inline-Count")));

      services.AddScoped<ICustomersRepository, CustomersRepository>();
      services.AddScoped<IStatesRepository, StatesRepository>();
      services.AddTransient<CustomersDbSeeder>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app,
      IWebHostEnvironment env,
      CustomersDbSeeder customersDbSeeder,
      IAntiforgery antiforgery)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      // This would need to be locked down as needed (very open right now)
      app.UseCors("AllowAllPolicy");

      app.UseStaticFiles();
      if (!env.IsDevelopment())
      {
        app.UseSpaStaticFiles();
      }

      // Enable middleware to serve generated Swagger as a JSON endpoint
      app.UseSwagger();

      // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
      // Visit http://localhost:5000/swagger
      app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

      // Manually handle setting XSRF cookie. Needed because HttpOnly
      // has to be set to false so that Angular is able to read/access the cookie.
      app.Use((context, next) =>
      {
        string path = context.Request.Path.Value;
        if (path != null && !path.ToLower().Contains("/api"))
        {
          var tokens = antiforgery.GetAndStoreTokens(context);
          context.Response.Cookies.Append("XSRF-TOKEN",
            tokens.RequestToken, new CookieOptions {HttpOnly = false}
          );
        }

        return next();
      });

      // For 3.0
      app.UseRouting();

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

      app.UseSpa(spa =>
      {
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501

        spa.Options.SourcePath = "Client";

        if (env.IsDevelopment())
        {
          spa.UseAngularCliServer(npmScript: "start");
        }
      });

      customersDbSeeder.SeedAsync(app.ApplicationServices).Wait();
    }
  }
}