using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    // [Authorize]
    public class HomeController : Controller
    {
        // private IRestaurantData _restaurantData;
        private IOdeToFoodRepository _restaurantData;
        private IGreeter _greeter;

        // previous code for in memory or non-repository pattern data
        // public HomeController(IRestaurantData restaurantData, IGreeter greeter)
        public HomeController(IOdeToFoodRepository restaurantData, IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        // [AllowAnonymous]
        public IActionResult Index()
        {
            var model = new HomeIndexViewModel();
            model.Restaurants = _restaurantData.GetAllRestaurants();
            model.CurrentMessage = _greeter.GetMessageOfTheDay();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            //var model = _restaurantData.Get(id);
            //if (model == null)
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(model);
            return null;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var newRestaurant = new Restaurant
            //    {
            //        Name = model.Name,
            //        Cuisine = model.Cuisine
            //    };

            //    newRestaurant = _restaurantData.Add(newRestaurant);

            //    return RedirectToAction(nameof(Details), new { id = newRestaurant.Id });
            //}
            //else
            //{
            //    return View();
            //}
            return null;

        }
    }
}
