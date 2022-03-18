using SharedModels;
using System.Collections.Generic;
using static SharedModel.Enums.Enums;

namespace SharedModel.Messages
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public IList<OrderLine> Products { get; set; }
    }
}
