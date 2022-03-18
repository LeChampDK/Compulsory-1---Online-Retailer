﻿using SharedModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModel
{
    public class CustomerExistAccepted
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public IList<OrderLine> OrderLines { get; set; }
    }
}
