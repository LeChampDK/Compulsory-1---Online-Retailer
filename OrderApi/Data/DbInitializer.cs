﻿using System.Collections.Generic;
using System.Linq;
using OrderApi.Models;
using System;
using OrderApi.Data.Facade;

namespace OrderApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(OrderApiContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any Products
            if (context.Orders.Any())
            {
                return;   // DB has been seeded
            }

            List<Order> orders = new List<Order>
            {
                new Order
                {
                    Date = DateTime.Today,
                    Products = new List<OrderProducts>{
                        new OrderProducts{
                            ProductId = 1,
                            Quantity = 2
                        }
                    }
                }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }
    }
}
