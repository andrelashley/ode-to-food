using Microsoft.AspNetCore.Hosting;
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

        public OdeToFoodSeeder(OdeToFoodDbContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            _ctx.Database.EnsureCreated();

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
