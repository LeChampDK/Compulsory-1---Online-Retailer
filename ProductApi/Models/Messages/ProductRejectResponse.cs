using System.Collections.Generic;
using static ProductApi.Enums;

namespace ProductApi.Models.Messages
{
    public class ProductRejectResponse
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
