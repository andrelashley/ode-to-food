using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Data
{
    public class OdeToFoodSeeder
    {
        private OdeToFoodDbContext _ctx;
        private IHostingEnvironment _hosting;
        private readonly UserManager<AppUser> _userManager;

        public OdeToFoodSeeder(OdeToFoodDbContext ctx,
            IHostingEnvironment hosting,
            UserManager<AppUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            _ctx.Database.EnsureCreated();

            var user = await _userManager.FindByEmailAsync("andre.lashley@gmail.com");

            if (user == null)
            {
                user = new AppUser()
                {
                    FirstName = "Andre",
                    LastName = "Lashley",
                    UserName = "andre.lashley@gmail.com",
                    Email = "andre.lashley@gmail.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user.");
                }
            }

            if (!_ctx.Restaurants.Any())
            {
                // Need to create sample data
                var filePath = Path.Combine(_hosting.ContentRootPath, "Data/restaurants.json");
                var json = File.ReadAllText(filePath);
                var restaurants = JsonConvert.DeserializeObject<IEnumerable<Restaurant>>(json);
                _ctx.Restaurants.AddRange(restaurants);

                _ctx.SaveChanges();
            }
        }
    }
}
