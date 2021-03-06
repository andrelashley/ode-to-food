﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OdeToFood.Data;
using OdeToFood.Models;
using OdeToFood.Services;
using System.Text;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _configuration;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAuthentication(options => 
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //})
            //.AddOpenIdConnect(options => 
            //{
            //    _configuration.Bind("AzureAd", options);
            //})
            //.AddCookie();

            services.AddIdentity<AppUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<OdeToFoodDbContext>();

            // support cookie auth and jwt auth
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg => 
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _configuration["Tokens:Issuer"],
                        ValidAudience = _configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]))
                    };
                });


            services.AddSingleton<IGreeter, Greeter>();

            // sql server setup
            services.AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));

            // DI for interfacing with EF core
            // services.AddScoped<IRestaurantData, SqlRestaurantData>();

            // DI for interfacing with EF core (Repository pattern)
            services.AddScoped<IOdeToFoodRepository, OdeToFoodRepository>();

            // DI for interfacing with in memory data
            // services.AddSingleton<IRestaurantData, InMemoryRestaurantData>();

            // DI for seeder class
            services.AddTransient<OdeToFoodSeeder>();

            services.AddMvc()
                // circumvent exceptions caused by referencing loops (in parent-child relationships)
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IGreeter greeter, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // force ssl
            app.UseRewriter(new RewriteOptions()
                .AddRedirectToHttpsPermanent());

            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

            app.UseAuthentication();

            app.UseMvc(ConfigureRoutes);

            if (env.IsDevelopment())
            {
                // seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<OdeToFoodSeeder>();
                    seeder.Seed().Wait();
                }
            }
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /Home/Index/4

            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
