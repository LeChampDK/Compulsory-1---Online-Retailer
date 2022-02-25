using System;
using static OrderApi.Enums.Enums;

namespace OrderApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public OrderStatus Status { get; set; }
    }
}
