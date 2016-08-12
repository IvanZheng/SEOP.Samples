using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvents
{
    public class ProductReduced : EventBase
    {
        public int ReduceCount { get; set; }
        public int Count { get; set; }

        public ProductReduced(Guid productId, int reduceCount, int count)
            : base(productId)
        {
            ReduceCount = reduceCount;
            Count = count;
            Key = productId.ToString();
        }
    }
}
