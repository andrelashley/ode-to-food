﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Models;
using System;
using System.Collections.Generic;

namespace OdeToFood.Controllers
{
    [Route("api/[Controller]")]
    public class RestaurantsController : Controller
    {
        private IOdeToFoodRepository _repository;
        private ILogger<RestaurantsController> _logger;

        public RestaurantsController(IOdeToFoodRepository repository, ILogger<RestaurantsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllRestaurants());
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

                if (restaurant == null) NotFound();
                return Ok(restaurant);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get restaurants: {ex}");
                return BadRequest($"Failed to get restaurant with id: {id}");
            }
        }
    }
}