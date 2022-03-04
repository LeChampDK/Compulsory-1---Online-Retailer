using System;
using static OrderApi.Enums.Enums;
using System.Collections.Generic;

namespace OrderApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderProducts> Products { get; set; }
        public int Quantity { get; internal set; }
    }
}
