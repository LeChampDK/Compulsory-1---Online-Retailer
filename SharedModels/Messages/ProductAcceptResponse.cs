using System.Collections.Generic;
using static SharedModel.Enums.Enums;

namespace SharedModel.Messages
{
    public class ProductAcceptResponse
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
    }
}
