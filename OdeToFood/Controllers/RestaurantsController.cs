using AutoMapper;
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
        private IMapper _mapper;

        public RestaurantsController(IOdeToFoodRepository repository,
                ILogger<RestaurantsController> logger,
                IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok( _mapper.Map<IEnumerable<Restaurant>, ICollection<RestaurantViewModel>>( _repository.GetAllRestaurants()));
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
                return Ok(_mapper.Map<Restaurant, RestaurantViewModel>(restaurant));
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
                    var newRestaurant = _mapper.Map<RestaurantViewModel, Restaurant>(model);
                    
                    _repository.AddEntity(newRestaurant);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/restaurants/{newRestaurant.Id}", _mapper.Map<Restaurant, RestaurantViewModel>(newRestaurant));
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