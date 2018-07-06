using Microsoft.Extensions.Logging;
using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Data
{
    public class OdeToFoodRepository : IOdeToFoodRepository
    {
        private OdeToFoodDbContext _ctx;
        private ILogger<OdeToFoodRepository> _logger;

        public OdeToFoodRepository(OdeToFoodDbContext ctx, ILogger<OdeToFoodRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Restaurant> GetAllRestaurants()
        {
            try
            {
                return _ctx.Restaurants
                    .OrderBy(r => r.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all resaurants: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
