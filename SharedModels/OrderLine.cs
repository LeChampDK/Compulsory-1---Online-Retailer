using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModel
{
    public class OrderLine
    {
        public int id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
