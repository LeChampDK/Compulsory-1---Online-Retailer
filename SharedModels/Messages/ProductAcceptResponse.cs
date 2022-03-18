using System.Collections.Generic;
using static ProductApi.Enums;

namespace SharedModel.Messages
{
    public class ProductAcceptResponse
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
