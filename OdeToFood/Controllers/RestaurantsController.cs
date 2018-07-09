using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Models;
using OdeToFood.ViewModels;
using System;
using System.Collections.Generic;

namespace OdeToFood.Controllers
{
    [Route("api/[Controller]")]
    public class RestaurantsController : Controller
    {
        private IOdeToFoodRepository _repository;
        private ILogger<RestaurantsController> _logger;

        public RestaurantsController(IOdeToFoodRepository repository,
                ILogger<RestaurantsController> logger)
        {
            _repository = repository;
            _logger = logger;

        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok( _repository.GetAllRestaurants());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get restaurants: {ex}");
                return BadRequest("Failed to get restaurants");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var restaurant = _repository.GetRestaurantById(id);

                if (restaurant == null) return NotFound();

                var vm = new RestaurantViewModel
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Cuisine = restaurant.Cuisine
                };
                return Ok(vm);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get restaurants: {ex}");
                return BadRequest($"Failed to get restaurant with id: {id}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] RestaurantViewModel model)
        {
            // add it to the db
            try
            {
                if (ModelState.IsValid)
                {
                    var newRestaurant = new Restaurant
                    {
                        Name = model.Name,
                        Cuisine = model.Cuisine
                    };

                    _repository.AddEntity(newRestaurant);
                    var vm = new RestaurantViewModel
                    {
                        Name = newRestaurant.Name,
                        Cuisine = newRestaurant.Cuisine
                    };
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/restaurants/{newRestaurant.Id}", vm);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new restaurant: {ex}");
            }
            return BadRequest("Failed to save new restaurant");
        }
    }
}