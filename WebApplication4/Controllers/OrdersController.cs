using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Data;

namespace WebApplication4.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
                //return Ok(_repository.GetAllProducts());
            }
            catch (Exception exp)
            {
                _logger.LogError("GetAllOrders() failed : " + exp.ToString());
                return BadRequest("Failed to get all orders  ");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                //return Ok(_repository.GetOrderById(id));
                var order = _repository.GetOrderById(id);

                if (order != null)
                    return Ok(order);
                else
                    return NotFound();
                //return Ok(_repository.GetAllProducts());
            }
            catch (Exception exp)
            {
                _logger.LogError("Get() failed : " + exp.ToString());
                return BadRequest("Failed to get specific order  ");
            }
        }

    }
}
