using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.Commands.Products
{
    public class GetTopProducts
    {
        public int Count { get; set; }
    }
    public class GetProducts
    {
        public List<Guid> ProductIds { get; set; }
    }
}
