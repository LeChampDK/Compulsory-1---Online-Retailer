using System.Collections.Generic;
using static ProductApi.Enums;

namespace SharedModel.Messages
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderProducts> Products { get; set; }
    }

    public class OrderProducts
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
