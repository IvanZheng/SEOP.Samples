﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.Commands.Products
{
    public class ReduceProduct
    {
        public Guid ProductId { get; set; }
        public int ReduceCount { get; set; }
    }
}
