using MyEvents;
using SEOP.Framework.Event;
using SEOP.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyEventSubscribers
{
    public class MyEventSubscriber : IEventSubscriber<MessageSubmitted>,
                                     IEventSubscriber<ProductReduced>
    {
        public static int HandledMessageCount;

        public void Handle(ProductReduced message)
        {
            Console.WriteLine($"{message.ToJson()}");
        }

        public void Handle(MessageSubmitted message)
        {
            Console.WriteLine($"{message.ToJson()}");
            Interlocked.Add(ref HandledMessageCount, 1);
        }
    }
}
