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

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            // services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnectionString"));
            });
            services.AddControllers();
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.AddAntiforgery(options => { options.HeaderName = "X-XSRF-TOKEN"; });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Application API",
                    Description = "Application Documentation",
                    Contact = new OpenApiContact {Name = "Dimka"},
                    License = new OpenApiLicense
                        {Name = "MIT", Url = new Uri("https://en.wikipedia.org/wiki/MIT_License")}
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

            services.AddScoped<IBoardGameRepository, BoardGameRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IBoardGameGenreRepository, BoardGameGenreRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("Home/Error");
            }


            app.UseCors("AllowAllPolicy");

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dimka's API V1"); });


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


            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}