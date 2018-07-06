using System.Collections.Generic;
using OdeToFood.Models;

namespace OdeToFood.Data
{
    public interface IOdeToFoodRepository
    {
        IEnumerable<Restaurant> GetAllRestaurants();
        bool SaveAll();
        Restaurant GetRestaurantById(int id);
        void AddEntity(object model);
    }
}