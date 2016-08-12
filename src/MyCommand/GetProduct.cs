using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommand
{
    public class GetTopProducts : CommandBase
    {
        public int Count { get; set; }
    }
    public class GetProducts : CommandBase
    {
        public List<Guid> ProductIds { get; set; }
    }
}
