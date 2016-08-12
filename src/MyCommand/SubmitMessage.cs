using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommand
{
    public class SubmitMessage : CommandBase
    {
        public string MessageBody { get; private set; }
        public DateTime SubmittedTime { get; private set; }
        public SubmitMessage(string messageBody)
        {
            MessageBody = messageBody;
            SubmittedTime = DateTime.Now;
        }
    }
}
