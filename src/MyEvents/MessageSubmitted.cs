using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvents
{
    public class MessageSubmitted : EventBase
    {
        public string MessageBody { get; private set; }
        public DateTime SubmittedTime { get; private set; }

        public MessageSubmitted(string messageBody)
        {
            MessageBody = messageBody;
            SubmittedTime = DateTime.Now;
        }

    }
}
