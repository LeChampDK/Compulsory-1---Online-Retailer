using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Data;
using OrderApi.Data.Facade;
using SharedModels;
using OrderApi.Service.Facade;
using RestSharp;
using OrderApi.Infrastructure;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService<Order> _orderService;
        IServiceGateway<ProductDto> productServiceGateway;
        IMessagePublisher messagePublisher;

        public OrdersController(IOrderService<Order> orderService,
            IServiceGateway<ProductDto> gateway,
            IMessagePublisher publisher)
        {
            _orderService = orderService;
            productServiceGateway = gateway;
            messagePublisher = publisher;
        }

        // GET: orders
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _orderService.GetAll();
        }

        // GET orders/5
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var item = _orderService.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST orders
        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            var result = _orderService.PostOrder(order);

            return CreatedAtRoute("GetProduct", new { id = result.Id }, result);
        }
    }
}
