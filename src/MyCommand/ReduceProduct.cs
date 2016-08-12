
using SEOP.Framework.Command;
using SEOP.Framework.Command.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommand
{
    public class ReduceProduct : ConcurrentCommand
    {
        [LinearKey]
        public Guid ProductId { get; set; }
        public int ReduceCount { get; set; }


    }
}
